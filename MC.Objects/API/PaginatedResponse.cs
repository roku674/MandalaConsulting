// Copyright © Mandala Consulting, Ltd., 2025. All Rights Reserved.
using System;
using System.Collections.Generic;

namespace MandalaConsulting.Objects
{
    /// <summary>
    /// Represents a paginated response from an API
    /// </summary>
    /// <typeparam name="T">The type of data being paginated</typeparam>
    public class PaginatedResponse<T>
    {
        /// <summary>
        /// The data items for this page
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// The current page number (1-based)
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// The number of items per page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The total number of items across all pages
        /// </summary>
        public long TotalItems { get; set; }

        /// <summary>
        /// The total number of pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Whether there is a next page available
        /// </summary>
        public bool HasNextPage => CurrentPage < TotalPages;

        /// <summary>
        /// Whether there is a previous page available
        /// </summary>
        public bool HasPreviousPage => CurrentPage > 1;

        /// <summary>
        /// Creates a new paginated response
        /// </summary>
        public PaginatedResponse()
        {
            Data = new List<T>();
        }

        /// <summary>
        /// Creates a new paginated response with data
        /// </summary>
        /// <param name="data">The data items for this page</param>
        /// <param name="currentPage">The current page number</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <param name="totalItems">The total number of items</param>
        public PaginatedResponse(List<T> data, int currentPage, int pageSize, long totalItems)
        {
            Data = data ?? new List<T>();
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalItems = totalItems;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        }
    }
}