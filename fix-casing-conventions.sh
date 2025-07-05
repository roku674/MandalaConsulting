#!/bin/bash

# Script to fix C# property casing conventions
# Structs: lowercase first letter
# Classes: uppercase first letter

echo "Fixing C# property casing conventions..."

# Function to convert first letter to lowercase
to_lowercase_first() {
    echo "$1" | sed 's/^\(.\)/\L\1/'
}

# Function to convert first letter to uppercase
to_uppercase_first() {
    echo "$1" | sed 's/^\(.\)/\U\1/'
}

# Find all C# files
find . -name "*.cs" -type f | while read -r file; do
    # Skip bin and obj directories
    if [[ "$file" == *"/bin/"* ]] || [[ "$file" == *"/obj/"* ]]; then
        continue
    fi
    
    # Create a temporary file
    temp_file=$(mktemp)
    
    # Process the file
    in_struct=false
    in_class=false
    brace_count=0
    
    while IFS= read -r line; do
        # Check if we're entering a struct
        if [[ "$line" =~ ^[[:space:]]*public[[:space:]]+struct[[:space:]]+ ]] || 
           [[ "$line" =~ ^[[:space:]]*internal[[:space:]]+struct[[:space:]]+ ]] ||
           [[ "$line" =~ ^[[:space:]]*private[[:space:]]+struct[[:space:]]+ ]] ||
           [[ "$line" =~ ^[[:space:]]*struct[[:space:]]+ ]]; then
            in_struct=true
            in_class=false
            brace_count=0
        fi
        
        # Check if we're entering a class
        if [[ "$line" =~ ^[[:space:]]*public[[:space:]]+class[[:space:]]+ ]] || 
           [[ "$line" =~ ^[[:space:]]*internal[[:space:]]+class[[:space:]]+ ]] ||
           [[ "$line" =~ ^[[:space:]]*private[[:space:]]+class[[:space:]]+ ]] ||
           [[ "$line" =~ ^[[:space:]]*abstract[[:space:]]+class[[:space:]]+ ]] ||
           [[ "$line" =~ ^[[:space:]]*sealed[[:space:]]+class[[:space:]]+ ]] ||
           [[ "$line" =~ ^[[:space:]]*class[[:space:]]+ ]]; then
            in_class=true
            in_struct=false
            brace_count=0
        fi
        
        # Count braces to track scope
        if [[ "$in_struct" == true ]] || [[ "$in_class" == true ]]; then
            brace_count=$((brace_count + $(echo "$line" | tr -cd '{' | wc -c)))
            brace_count=$((brace_count - $(echo "$line" | tr -cd '}' | wc -c)))
            
            # Exit struct/class when brace count reaches 0
            if [[ $brace_count -le 0 ]]; then
                in_struct=false
                in_class=false
            fi
        fi
        
        # Process properties in structs (make lowercase)
        if [[ "$in_struct" == true ]] && [[ "$line" =~ ^[[:space:]]*public[[:space:]]+[^[:space:]]+[[:space:]]+([A-Z][a-zA-Z0-9_]*)[[:space:]]*\{[[:space:]]*get ]]; then
            prop_name="${BASH_REMATCH[1]}"
            new_prop_name=$(to_lowercase_first "$prop_name")
            line="${line//$prop_name/$new_prop_name}"
            echo "  Struct property: $prop_name -> $new_prop_name in $file"
        fi
        
        # Process properties in classes (make uppercase)
        if [[ "$in_class" == true ]] && [[ "$line" =~ ^[[:space:]]*public[[:space:]]+[^[:space:]]+[[:space:]]+([a-z][a-zA-Z0-9_]*)[[:space:]]*\{[[:space:]]*get ]]; then
            prop_name="${BASH_REMATCH[1]}"
            # Skip special cases like 'message' in ResponseData
            if [[ "$prop_name" != "message" ]]; then
                new_prop_name=$(to_uppercase_first "$prop_name")
                line="${line//$prop_name/$new_prop_name}"
                echo "  Class property: $prop_name -> $new_prop_name in $file"
            fi
        fi
        
        echo "$line"
    done < "$file" > "$temp_file"
    
    # Replace the original file
    mv "$temp_file" "$file"
done

echo "Casing convention fix complete!"

# Now run a second pass to fix any references to these properties
echo "Fixing property references..."

# This is more complex and would need proper parsing, so let's create a list of known fixes
cat << 'EOF' > /tmp/property_fixes.txt
# Add specific property fixes here as needed
# Format: ClassName|OldProperty|NewProperty
EOF

echo "Note: You may need to manually fix property references in code."
echo "The script has fixed property declarations, but usages may need manual review."