// <copyright file="SqlDataProvider.cs" company="Engage Software">
// Engage: Rotator - http://www.engagemodules.com
// Copyright (c) 2004-2014
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

namespace Engage.Dnn.ContentRotator
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Framework.Providers;
    using Microsoft.ApplicationBlocks.Data;

    /// <summary>
    /// SQL Server implementation of the abstract <see cref="DataProvider"/> class
    /// </summary>
    public class SqlDataProvider : DataProvider
    {
        /// <summary>
        /// The prefix used for database objects belonging to this module
        /// </summary>
        private const string ModuleQualifier = "EngageRotator_";

        /// <summary>
        /// The size of text fields in the database
        /// </summary>
        private const int TextFieldSize = 255;

        /// <summary>
        /// The connection string to access the database
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The prefix for all DNN database objects
        /// </summary>
        private readonly string objectQualifier;

        /// <summary>
        /// The owner or schema name to prefix object with
        /// </summary>
        private readonly string databaseOwner;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDataProvider"/> class.
        /// </summary>
        public SqlDataProvider()
        {
            ProviderConfiguration dataProviderConfigurationSection = ProviderConfiguration.GetProviderConfiguration("data");
            var defaultDataProviderConfiguration = (Provider)dataProviderConfigurationSection.Providers[dataProviderConfigurationSection.DefaultProvider];
            this.connectionString = Config.GetConnectionString();
            if (string.IsNullOrEmpty(this.connectionString))
            {
                this.connectionString = defaultDataProviderConfiguration.Attributes["connectionString"];
            }

            this.objectQualifier = defaultDataProviderConfiguration.Attributes["objectQualifier"];
            if (!string.IsNullOrEmpty(this.objectQualifier) && !this.objectQualifier.EndsWith("_", StringComparison.OrdinalIgnoreCase))
            {
                this.objectQualifier += "_";
            }

            this.databaseOwner = defaultDataProviderConfiguration.Attributes["databaseOwner"];
            if (!string.IsNullOrEmpty(this.databaseOwner) && !this.databaseOwner.EndsWith(".", StringComparison.OrdinalIgnoreCase))
            {
                this.databaseOwner += ".";
            }
        }

        /// <summary>
        /// Gets the name prefix for Engage: Rotator database objects.
        /// </summary>
        /// <value>The name prefix for Engage: Rotator database objects.</value>
        public string NamePrefix
        {
            get
            {
                return this.databaseOwner + this.objectQualifier + ModuleQualifier;
            }
        }

        /// <summary>
        /// Inserts a new slide.
        /// </summary>
        /// <param name="content">The main content being displayed.</param>
        /// <param name="imageUrl">The URL to the main image.</param>
        /// <param name="linkUrl">The link URL.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="moduleId">The module id.</param>
        /// <param name="title">The title.</param>
        /// <param name="pagerImageUrl">The URL to the pager image.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="trackLink">Whether to track clicks to <paramref name="linkUrl"/></param>
        /// <returns>
        /// The ID of the slide created in the database
        /// </returns>
        public override int InsertSlide(string content, string imageUrl, string linkUrl, DateTime startDate, DateTime? endDate, int moduleId, string title, string pagerImageUrl, int sortOrder, bool trackLink)
        {
            return (int)(decimal)this.ExecuteScalar(
                "InsertSlide",
                Engage.Utility.CreateTextParam("@content", content),
                Engage.Utility.CreateVarcharParam("@imageUrl", imageUrl, TextFieldSize),
                Engage.Utility.CreateVarcharParam("@pagerImageUrl", pagerImageUrl, TextFieldSize),
                Engage.Utility.CreateVarcharParam("@linkUrl", linkUrl, TextFieldSize),
                Engage.Utility.CreateDateTimeParam("@startDate", startDate),
                Engage.Utility.CreateDateTimeParam("@endDate", endDate),
                Engage.Utility.CreateIntegerParam("@moduleId", moduleId),
                Engage.Utility.CreateVarcharParam("@title", title, TextFieldSize),
                Engage.Utility.CreateIntegerParam("@sortOrder", sortOrder),
                Engage.Utility.CreateBitParam("@trackLink", trackLink));
        }

        /// <summary>
        /// Updates the slide with the given <see cref="slideId"/>.
        /// </summary>
        /// <param name="slideId">The ID of the slide to update.</param>
        /// <param name="content">The main content being displayed.</param>
        /// <param name="imageUrl">The URL to the main image.</param>
        /// <param name="linkUrl">The link URL.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="title">The title.</param>
        /// <param name="pagerImageUrl">The URL to the pager image.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="trackLink">Whether to track clicks to <paramref name="linkUrl"/></param>
        public override void UpdateSlide(int slideId, string content, string imageUrl, string linkUrl, DateTime startDate, DateTime? endDate, string title, string pagerImageUrl, int sortOrder, bool trackLink)
        {
            this.ExecuteNonQuery(
                "UpdateSlide",
                Engage.Utility.CreateIntegerParam("@slideId", slideId),
                Engage.Utility.CreateTextParam("@content", content),
                Engage.Utility.CreateVarcharParam("@imageUrl", imageUrl, TextFieldSize),
                Engage.Utility.CreateVarcharParam("@linkUrl", linkUrl, TextFieldSize),
                Engage.Utility.CreateDateTimeParam("@startDate", startDate),
                Engage.Utility.CreateDateTimeParam("@endDate", endDate),
                Engage.Utility.CreateVarcharParam("@title", title, TextFieldSize),
                Engage.Utility.CreateVarcharParam("@pagerImageUrl", pagerImageUrl, TextFieldSize),
                Engage.Utility.CreateIntegerParam("@sortOrder", sortOrder),
                Engage.Utility.CreateBitParam("@trackLink", trackLink));
        }

        /// <summary>
        /// Deletes the slide with the given <paramref name="slideId"/>.
        /// </summary>
        /// <param name="slideId">The ID of the slide to delete.</param>
        public override void DeleteSlide(int slideId)
        {
            this.ExecuteNonQuery(
                "DeleteSlide",
                Engage.Utility.CreateIntegerParam("@slideId", slideId));
        }

        /// <summary>
        /// Gets the slide with the given <paramref name="slideId"/>.
        /// </summary>
        /// <param name="slideId">The ID of the slide to retrieve.</param>
        /// <returns>
        /// The slide with the given <paramref name="slideId"/>
        /// </returns>
        public override IDataReader GetSlide(int slideId)
        {
            return this.ExecuteReader(
                "GetSlide",
                Engage.Utility.CreateIntegerParam("@slideId", slideId));
        }

        /// <summary>
        /// Gets all of the slides for the given <paramref name="moduleId"/>, getting either only slides which have started but not ended, or all slides if <paramref name="getOutdatedSlides"/> is true.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <param name="getOutdatedSlides">if set to <c>true</c> gets all slides, regardless of their start date or end date, otherwise only returns slides that have started but not ended.</param>
        /// <returns>
        /// All of the slides for the given <paramref name="moduleId"/>
        /// </returns>
        public override IDataReader GetSlides(int moduleId, bool getOutdatedSlides)
        {
            return this.ExecuteReader(
                "GetSlides",
                Engage.Utility.CreateIntegerParam("@moduleId", moduleId),
                Engage.Utility.CreateBitParam("@getOutdatedSlides", getOutdatedSlides));
        }

        /// <summary>
        /// Executes a SQL stored procedure without returning any value.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.  Does not include any prefix, for example <c>InsertSlide</c> is translated to <c>dnn_EngageRotator_spInsertSlide</c>.</param>
        /// <param name="parameters">The parameters for this query.</param>
        private void ExecuteNonQuery(string storedProcedureName, params SqlParameter[] parameters)
        {
            SqlHelper.ExecuteNonQuery(
                this.connectionString,
                CommandType.StoredProcedure,
                this.NamePrefix + "sp" + storedProcedureName,
                parameters);
        }

        /// <summary>
        /// Executes a SQL stored procedure, returning the results as a <see cref="SqlDataReader"/>.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.  Does not include any prefix, for example <c>GetSlide</c> is translated to <c>dnn_EngageRotator_spGetSlide</c>.</param>
        /// <param name="parameters">The parameters for this query.</param>
        /// <returns>A <see cref="SqlDataReader"/> with the results of the stored procedure call</returns>
        private SqlDataReader ExecuteReader(string storedProcedureName, params SqlParameter[] parameters)
        {
            return SqlHelper.ExecuteReader(
                this.connectionString,
                CommandType.StoredProcedure,
                this.NamePrefix + "sp" + storedProcedureName,
                parameters);
        }

        /// <summary>
        /// Executes a SQL stored procedure, returning a single value.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.  Does not include any prefix, for example <c>InsertSlide</c> is translated to <c>dnn_EngageRotator_spInsertSlide</c>.</param>
        /// <param name="parameters">The parameters for this query.</param>
        /// <returns>The single value returned from the stored procedure call</returns>
        private object ExecuteScalar(string storedProcedureName, params SqlParameter[] parameters)
        {
            return SqlHelper.ExecuteScalar(
                this.connectionString,
                CommandType.StoredProcedure,
                this.NamePrefix + "sp" + storedProcedureName,
                parameters);
        }
    }
}