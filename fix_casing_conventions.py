#!/usr/bin/env python3

import os
import re
from pathlib import Path

def find_cs_files(root_dir):
    """Find all C# files, excluding bin and obj directories"""
    cs_files = []
    for root, dirs, files in os.walk(root_dir):
        # Skip bin and obj directories
        dirs[:] = [d for d in dirs if d not in ['bin', 'obj', '.git']]
        
        for file in files:
            if file.endswith('.cs'):
                cs_files.append(os.path.join(root, file))
    
    return cs_files

def analyze_file(file_path):
    """Analyze a C# file for casing convention issues"""
    with open(file_path, 'r', encoding='utf-8') as f:
        content = f.read()
    
    lines = content.split('\n')
    
    # Patterns
    struct_pattern = r'(?:public|internal|private|protected)?\s*(?:readonly\s+)?struct\s+(\w+)'
    class_pattern = r'(?:public|internal|private|protected)?\s*(?:abstract\s+|sealed\s+|static\s+|partial\s+)*class\s+(\w+)'
    property_pattern = r'public\s+(?:virtual\s+|override\s+|static\s+)?(?!class|struct|interface|enum|delegate)(\S+)\s+(\w+)\s*\{\s*(?:get|set)'
    
    issues = []
    current_type = None
    current_type_name = ""
    brace_depth = 0
    
    for i, line in enumerate(lines):
        # Check for struct
        struct_match = re.search(struct_pattern, line)
        if struct_match:
            current_type = 'struct'
            current_type_name = struct_match.group(1)
            brace_depth = 0
        
        # Check for class
        class_match = re.search(class_pattern, line)
        if class_match:
            current_type = 'class'
            current_type_name = class_match.group(1)
            brace_depth = 0
        
        # Track brace depth
        if current_type:
            brace_depth += line.count('{') - line.count('}')
            if brace_depth <= 0:
                current_type = None
        
        # Check properties
        prop_match = re.search(property_pattern, line)
        if prop_match and current_type:
            prop_type = prop_match.group(1)
            prop_name = prop_match.group(2)
            
            if current_type == 'struct' and prop_name[0].isupper():
                # Struct property should be lowercase
                new_name = prop_name[0].lower() + prop_name[1:]
                issues.append({
                    'file': file_path,
                    'line': i + 1,
                    'type': 'struct',
                    'type_name': current_type_name,
                    'property': prop_name,
                    'should_be': new_name,
                    'full_line': line.strip()
                })
            elif current_type == 'class' and prop_name[0].islower() and prop_name != 'message':
                # Class property should be uppercase (except 'message')
                new_name = prop_name[0].upper() + prop_name[1:]
                issues.append({
                    'file': file_path,
                    'line': i + 1,
                    'type': 'class',
                    'type_name': current_type_name,
                    'property': prop_name,
                    'should_be': new_name,
                    'full_line': line.strip()
                })
    
    return issues

def fix_file(file_path, issues):
    """Fix casing issues in a file"""
    with open(file_path, 'r', encoding='utf-8') as f:
        content = f.read()
    
    # Group issues by file
    file_issues = [issue for issue in issues if issue['file'] == file_path]
    
    for issue in file_issues:
        old_name = issue['property']
        new_name = issue['should_be']
        
        # Replace property declaration
        pattern = rf'(\bpublic\s+\S+\s+){old_name}(\s*\{{)'
        replacement = rf'\1{new_name}\2'
        content = re.sub(pattern, replacement, content)
    
    with open(file_path, 'w', encoding='utf-8') as f:
        f.write(content)

def main():
    print("Scanning C# files for casing convention issues...")
    
    root_dir = os.getcwd()
    cs_files = find_cs_files(root_dir)
    
    all_issues = []
    
    for file_path in cs_files:
        issues = analyze_file(file_path)
        all_issues.extend(issues)
    
    if not all_issues:
        print("No casing convention issues found!")
        return
    
    # Group issues by type
    struct_issues = [i for i in all_issues if i['type'] == 'struct']
    class_issues = [i for i in all_issues if i['type'] == 'class']
    
    print("\n=== STRUCTS WITH UPPERCASE PROPERTIES (Should be lowercase) ===")
    for issue in struct_issues:
        rel_path = os.path.relpath(issue['file'], root_dir)
        print(f"\n{rel_path}:{issue['line']} - struct {issue['type_name']}")
        print(f"  {issue['property']} -> {issue['should_be']}")
        print(f"  Line: {issue['full_line']}")
    
    print("\n=== CLASSES WITH LOWERCASE PROPERTIES (Should be uppercase) ===")
    for issue in class_issues:
        rel_path = os.path.relpath(issue['file'], root_dir)
        print(f"\n{rel_path}:{issue['line']} - class {issue['type_name']}")
        print(f"  {issue['property']} -> {issue['should_be']}")
        print(f"  Line: {issue['full_line']}")
    
    print(f"\nFound {len(struct_issues)} struct issues and {len(class_issues)} class issues.")
    
    response = input("Would you like to automatically fix these? (y/n): ").lower()
    
    if response in ['y', 'yes']:
        print("\nFixing casing conventions...")
        
        # Get unique files
        files_to_fix = list(set(issue['file'] for issue in all_issues))
        
        for file_path in files_to_fix:
            fix_file(file_path, all_issues)
            print(f"Fixed: {os.path.relpath(file_path, root_dir)}")
        
        print("\nCasing conventions fixed!")
        print("\nNOTE: Property references in code may still need manual fixes.")
        print("Consider running build/tests to catch any reference issues.")
    else:
        print("\nNo changes made.")

if __name__ == "__main__":
    main()