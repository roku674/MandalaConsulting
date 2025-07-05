// Copyright © Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-10-12 10:20:40
// Edited by Alexander Fields https://www.alexanderfields.me 2025-07-02 11:48:25
//Created by Alexander Fields

using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MandalaConsulting.Repository.Mongo
{
    /// <summary>
    /// Interface for MongoDB database operations.
    /// Provides methods for CRUD operations, connection management, and database interactions.
    /// Make sure you set the MongoDB Instance before using the implementation.
    /// </summary>
    public interface IMongoHelper
    {
        /// <summary>
        /// Gets or sets the MongoDB database instance.
        /// </summary>
        IMongoDatabase database { get; set; }

        /// <summary>
        /// Gets or sets the name of the MongoDB database.
        /// </summary>
        public string dbName { get; set; }

        /// <summary>
        /// Creates a new document in the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of document to create.</typeparam>
        /// <param name="collectionName">The name of the collection.</param>
        /// <param name="document">The document to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CreateDocumentAsync<T>(string collectionName, T document);

        /// <summary>
        /// Creates a new MongoDB database instance.
        /// </summary>
        /// <param name="dbName">The name of the database.</param>
        /// <param name="connectionString">The MongoDB connection string.</param>
        /// <returns>The MongoDB database instance.</returns>
        IMongoDatabase CreateMongoDbInstance(string dbName, string connectionString);

        /// <summary>
        /// Deletes a document from the specified collection that matches the filter.
        /// </summary>
        /// <typeparam name="T">The type of document to delete.</typeparam>
        /// <param name="collectionName">The name of the collection.</param>
        /// <param name="filter">The filter to match documents for deletion.</param>
        /// <returns>The result of the delete operation.</returns>
        Task<DeleteResult> DeleteDocumentAsync<T>(string collectionName, FilterDefinition<T> filter);

        /// <summary>
        /// Gets all documents from the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of documents to retrieve.</typeparam>
        /// <param name="collectionName">The name of the collection.</param>
        /// <returns>A list of all documents in the collection.</returns>
        Task<List<T>> GetAllDocumentsAsync<T>(string collectionName);

        /// <summary>
        /// Gets documents from the specified collection that match the filter.
        /// </summary>
        /// <typeparam name="T">The type of documents to retrieve.</typeparam>
        /// <param name="collectionName">The name of the collection.</param>
        /// <param name="filter">The filter to match documents.</param>
        /// <returns>A list of matching documents.</returns>
        Task<List<T>> GetFilteredDocumentsAsync<T>(string collectionName, FilterDefinition<T> filter);

        /// <summary>
        /// Replaces a document in the specified collection that matches the filter.
        /// </summary>
        /// <typeparam name="T">The type of document to replace.</typeparam>
        /// <param name="collectionName">The name of the collection.</param>
        /// <param name="filter">The filter to match the document to replace.</param>
        /// <param name="document">The new document.</param>
        /// <returns>The result of the replace operation.</returns>
        Task<ReplaceOneResult> ReplaceDocumentAsync<T>(string collectionName, FilterDefinition<T> filter, T document);

        /// <summary>
        /// Tests the connection to the database
        /// </summary>
        /// <returns>Will return a List of Collection Names if it worked otherwise returns null</returns>
        List<string> TestConnection();

        /// <summary>
        /// Updates a document in the specified collection that matches the filter.
        /// </summary>
        /// <typeparam name="T">The type of document to update.</typeparam>
        /// <param name="collectionName">The name of the collection.</param>
        /// <param name="filter">The filter to match the document to update.</param>
        /// <param name="updateDefinition">The update definition.</param>
        /// <returns>The result of the update operation.</returns>
        Task<UpdateResult> UpdateDocumentAsync<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> updateDefinition);
    }
}
