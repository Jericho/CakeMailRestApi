﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class TemplatesTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[TestMethod]
		public void CreateTemplateCategory_with_minimal_parameters()
		{
			// Arrange
			var categoryId = 123;
			var labels = new Dictionary<string, string>()
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/CreateCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "templates_copyable" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[en_US]" && (string)p.Value == "My Category" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[fr_FR]" && (string)p.Value == "Ma Catégorie" && p.Type == ParameterType.GetOrPost) == 1
				))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", categoryId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateTemplateCategory(USER_KEY, labels);

			// Assert
			Assert.AreEqual(categoryId, result);
		}

		[TestMethod]
		public void CreateTemplateCategory_with_no_labels()
		{
			// Arrange
			var categoryId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/CreateCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "templates_copyable" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1
				))).Returns(new RestResponse()
				{
					StatusCode = HttpStatusCode.OK,
					ContentType = "json",
					Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", categoryId)
				});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateTemplateCategory(USER_KEY, null);

			// Assert
			Assert.AreEqual(categoryId, result);
		}

		[TestMethod]
		public void CreateTemplateCategory_with_isvisiblebydefault_false()
		{
			// Arrange
			var categoryId = 123;
			var labels = new Dictionary<string, string>()
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/CreateCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default" && (string)p.Value == "0" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "templates_copyable" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[en_US]" && (string)p.Value == "My Category" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[fr_FR]" && (string)p.Value == "Ma Catégorie" && p.Type == ParameterType.GetOrPost) == 1
				))).Returns(new RestResponse()
				{
					StatusCode = HttpStatusCode.OK,
					ContentType = "json",
					Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", categoryId)
				});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateTemplateCategory(USER_KEY, labels, isVisibleByDefault: false);

			// Assert
			Assert.AreEqual(categoryId, result);
		}

		[TestMethod]
		public void CreateTemplateCategory_with_templatescanbecopied_false()
		{
			// Arrange
			var categoryId = 123;
			var labels = new Dictionary<string, string>()
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/CreateCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "templates_copyable" && (string)p.Value == "0" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[en_US]" && (string)p.Value == "My Category" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[fr_FR]" && (string)p.Value == "Ma Catégorie" && p.Type == ParameterType.GetOrPost) == 1
				))).Returns(new RestResponse()
				{
					StatusCode = HttpStatusCode.OK,
					ContentType = "json",
					Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", categoryId)
				});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateTemplateCategory(USER_KEY, labels, templatesCanBeCopied: false);

			// Assert
			Assert.AreEqual(categoryId, result);
		}

		[TestMethod]
		public void CreateTemplateCategory_with_clientid()
		{
			// Arrange
			var categoryId = 123;
			var labels = new Dictionary<string, string>()
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/CreateCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "templates_copyable" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[en_US]" && (string)p.Value == "My Category" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[fr_FR]" && (string)p.Value == "Ma Catégorie" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
				))).Returns(new RestResponse()
				{
					StatusCode = HttpStatusCode.OK,
					ContentType = "json",
					Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", categoryId)
				});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateTemplateCategory(USER_KEY, labels, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(categoryId, result);
		}

		[TestMethod]
		public void DeleteTemplateCategory_with_minimal_parameters()
		{
			// Arrange
			var categoryId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/DeleteCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.DeleteTemplateCategory(USER_KEY, categoryId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void DeleteTemplateCategory_with_clientid()
		{
			// Arrange
			var categoryId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/DeleteCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.DeleteTemplateCategory(USER_KEY, categoryId, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void GetTemplateCategory_with_minimal_parameters()
		{
			// Arrange
			var categoryId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"owner_client_id\":\"{1}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}}}", categoryId, CLIENT_ID)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategory(USER_KEY, categoryId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(categoryId, result.Id);
		}

		[TestMethod]
		public void GetTemplateCategory_with_clientid()
		{
			// Arrange
			var categoryId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"owner_client_id\":\"{1}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}}}", categoryId, CLIENT_ID)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategory(USER_KEY, categoryId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(categoryId, result.Id);
		}

		[TestMethod]
		public void GetTemplateCategories_with_minimal_parameters()
		{
			// Arrange
			var jsonCategory1 = string.Format("{{\"id\":\"111\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonCategory2 = string.Format("{{\"id\":\"222\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #2\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategories/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"categories\":[{0},{1}]}}}}", jsonCategory1, jsonCategory2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategories(USER_KEY);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetTemplateCategories_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonCategory1 = string.Format("{{\"id\":\"111\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonCategory2 = string.Format("{{\"id\":\"222\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #2\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategories/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"categories\":[{0},{1}]}}}}", jsonCategory1, jsonCategory2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategories(USER_KEY, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetTemplateCategories_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonCategory1 = string.Format("{{\"id\":\"111\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonCategory2 = string.Format("{{\"id\":\"222\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #2\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategories/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"categories\":[{0},{1}]}}}}", jsonCategory1, jsonCategory2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategories(USER_KEY, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetTemplateCategories_with_clientid()
		{
			// Arrange
			var jsonCategory1 = string.Format("{{\"id\":\"111\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonCategory2 = string.Format("{{\"id\":\"222\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #2\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategories/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"categories\":[{0},{1}]}}}}", jsonCategory1, jsonCategory2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategories(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetTemplateCategoriesCount_with_minimal_parameters()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategories/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategoriesCount(USER_KEY);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetTemplateCategoriesCount_with_clientid()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategories/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategoriesCount(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void UpdateTemplateCategory_with_minimal_parameters()
		{
			// Arrange
			var categoryId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/SetCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "templates_copyable" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateTemplateCategory(USER_KEY, categoryId, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateTemplateCategory_with_labels()
		{
			// Arrange
			var categoryId = 123;
			var labels = new Dictionary<string, string>()
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/SetCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "templates_copyable" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[en_US]" && (string)p.Value == "My Category" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[fr_FR]" && (string)p.Value == "Ma Catégorie" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateTemplateCategory(USER_KEY, categoryId, labels);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateTemplateCategory_with_default_true()
		{
			// Arrange
			var categoryId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/SetCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "templates_copyable" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateTemplateCategory(USER_KEY, categoryId, null, isVisibleByDefault: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateTemplateCategory_with_default_false()
		{
			// Arrange
			var categoryId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/SetCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default" && (string)p.Value == "0" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "templates_copyable" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateTemplateCategory(USER_KEY, categoryId, null, isVisibleByDefault: false);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateTemplateCategory_with_copyable_true()
		{
			// Arrange
			var categoryId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/SetCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "templates_copyable" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateTemplateCategory(USER_KEY, categoryId, null, templatesCanBeCopied: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateTemplateCategory_with_copyable_false()
		{
			// Arrange
			var categoryId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/SetCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "templates_copyable" && (string)p.Value == "0" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateTemplateCategory(USER_KEY, categoryId, null, templatesCanBeCopied: false);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateTemplateCategory_with_clientid()
		{
			// Arrange
			var categoryId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/SetCategory/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "default" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "templates_copyable" && (string)p.Value == "1" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateTemplateCategory(USER_KEY, categoryId, null, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void GetTemplateCategoryVisibility_with_minimal_parameters()
		{
			// Arrange
			var categoryId = 12345;

			var jsonVisibility1 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #1\",\"visible\":\"1\"}";
			var jsonVisibility2 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #2\",\"visible\":\"0\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategoryVisibility/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonVisibility1, jsonVisibility2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategoryVisibility(USER_KEY, categoryId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.IsTrue(result.ToArray()[0].Visible);
			Assert.IsFalse(result.ToArray()[1].Visible);
		}

		[TestMethod]
		public void GetTemplateCategoryVisibility_with_limit()
		{
			// Arrange
			var categoryId = 12345;
			var limit = 5;

			var jsonVisibility1 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #1\",\"visible\":\"1\"}";
			var jsonVisibility2 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #2\",\"visible\":\"0\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategoryVisibility/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonVisibility1, jsonVisibility2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategoryVisibility(USER_KEY, categoryId, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetTemplateCategoryVisibility_with_offset()
		{
			// Arrange
			var categoryId = 12345;
			var offset = 25;

			var jsonVisibility1 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #1\",\"visible\":\"1\"}";
			var jsonVisibility2 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #2\",\"visible\":\"0\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategoryVisibility/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonVisibility1, jsonVisibility2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategoryVisibility(USER_KEY, categoryId, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetTemplateCategoryVisibility_with_clientid()
		{
			// Arrange
			var categoryId = 12345;

			var jsonVisibility1 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #1\",\"visible\":\"1\"}";
			var jsonVisibility2 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #2\",\"visible\":\"0\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategoryVisibility/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonVisibility1, jsonVisibility2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategoryVisibility(USER_KEY, categoryId, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetTemplateCategoryVisibilityCount_with_minimal_parameters()
		{
			// Arrange
			var categoryId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategoryVisibility/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategoryVisibilityCount(USER_KEY, categoryId);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetTemplateCategoryVisibilityCount_with_clientid()
		{
			// Arrange
			var categoryId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetCategoryVisibility/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplateCategoryVisibilityCount(USER_KEY, categoryId, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void CreateTemplate_with_minimal_parameters()
		{
			// Arrange
			var templateId = 123;
			var categoryId = 111;
			var content = "this is the content";
			var labels = new Dictionary<string, string>()
			{
				{ "en_US", "My Template" },
				{ "fr_FR", "Mon Modèle" }
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/CreateTemplate/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[en_US]" && (string)p.Value == "My Template" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[fr_FR]" && (string)p.Value == "Mon Modèle" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "content" && (string)p.Value == content && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1
				))).Returns(new RestResponse()
				{
					StatusCode = HttpStatusCode.OK,
					ContentType = "json",
					Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", templateId)
				});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateTemplate(USER_KEY, labels, content, categoryId);

			// Assert
			Assert.AreEqual(templateId, result);
		}

		[TestMethod]
		public void CreateTemplate_with_no_labels()
		{
			// Arrange
			var templateId = 123;
			var categoryId = 111;
			var content = "this is the content";
			var labels = new Dictionary<string, string>()
			{
				{ "en_US", "My Template" },
				{ "fr_FR", "Mon modèle" }
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/CreateTemplate/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "content" && (string)p.Value == content && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1
				))).Returns(new RestResponse()
				{
					StatusCode = HttpStatusCode.OK,
					ContentType = "json",
					Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", templateId)
				});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateTemplate(USER_KEY, null, content, categoryId);

			// Assert
			Assert.AreEqual(templateId, result);
		}

		[TestMethod]
		public void CreateTemplate_with_clientid()
		{
			// Arrange
			var templateId = 123;
			var categoryId = 111;
			var content = "this is the content";
			var labels = new Dictionary<string, string>()
			{
				{ "en_US", "My Template" },
				{ "fr_FR", "Mon Modèle" }
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/CreateTemplate/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[en_US]" && (string)p.Value == "My Template" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[fr_FR]" && (string)p.Value == "Mon Modèle" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "content" && (string)p.Value == content && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
				))).Returns(new RestResponse()
				{
					StatusCode = HttpStatusCode.OK,
					ContentType = "json",
					Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", templateId)
				});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.CreateTemplate(USER_KEY, labels, content, categoryId, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(templateId, result);
		}

		[TestMethod]
		public void DeleteTemplate_with_minimal_parameters()
		{
			// Arrange
			var templateId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/DeleteTemplate/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "template_id" && (long)p.Value == templateId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.DeleteTemplate(USER_KEY, templateId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void DeleteTemplate_with_clientid()
		{
			// Arrange
			var templateId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/DeleteTemplate/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "template_id" && (long)p.Value == templateId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.DeleteTemplate(USER_KEY, templateId, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void GetTemplate_with_minimal_parameters()
		{
			// Arrange
			var templateId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetTemplate/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "template_id" && (long)p.Value == templateId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"labels\":[],\"id\":\"{0}\",\"status\":\"active\",\"category_id\":\"7480\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}}}}", templateId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplate(USER_KEY, templateId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(templateId, result.Id);
		}

		[TestMethod]
		public void GetTemplate_with_clientid()
		{
			// Arrange
			var templateId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetTemplate/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "template_id" && (long)p.Value == templateId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"labels\":[],\"id\":\"{0}\",\"status\":\"active\",\"category_id\":\"7480\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}}}}", templateId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplate(USER_KEY, templateId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(templateId, result.Id);
		}

		[TestMethod]
		public void GetTemplates_with_minimal_parameters()
		{
			// Arrange
			var jsonTemplate1 = "{\"labels\":[],\"id\":\"111\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}";
			var jsonTemplate2 = "{\"labels\":[],\"id\":\"222\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"bbb\",\"description\":\"bbb\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetTemplates/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"templates\":[{0},{1}]}}}}", jsonTemplate1, jsonTemplate2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplates(USER_KEY);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetTemplates_with_categoryid()
		{
			// Arrange
			var categoryId = 123;
			var jsonTemplate1 = "{\"labels\":[],\"id\":\"111\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}";
			var jsonTemplate2 = "{\"labels\":[],\"id\":\"222\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"bbb\",\"description\":\"bbb\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetTemplates/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"templates\":[{0},{1}]}}}}", jsonTemplate1, jsonTemplate2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplates(USER_KEY, categoryId: categoryId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetTemplates_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonTemplate1 = "{\"labels\":[],\"id\":\"111\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}";
			var jsonTemplate2 = "{\"labels\":[],\"id\":\"222\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"bbb\",\"description\":\"bbb\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetTemplates/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"templates\":[{0},{1}]}}}}", jsonTemplate1, jsonTemplate2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplates(USER_KEY, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetTemplates_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonTemplate1 = "{\"labels\":[],\"id\":\"111\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}";
			var jsonTemplate2 = "{\"labels\":[],\"id\":\"222\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"bbb\",\"description\":\"bbb\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetTemplates/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"templates\":[{0},{1}]}}}}", jsonTemplate1, jsonTemplate2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplates(USER_KEY, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetTemplates_with_clientid()
		{
			// Arrange
			var jsonTemplate1 = "{\"labels\":[],\"id\":\"111\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}";
			var jsonTemplate2 = "{\"labels\":[],\"id\":\"222\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"bbb\",\"description\":\"bbb\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetTemplates/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"templates\":[{0},{1}]}}}}", jsonTemplate1, jsonTemplate2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplates(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public void GetTemplatesCount_with_minimal_parameters()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetTemplates/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplatesCount(USER_KEY);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetTemplatesCount_with_categoryid()
		{
			// Arrange
			var categoryId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetTemplates/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplatesCount(USER_KEY, categoryId: categoryId);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void GetTemplatesCount_with_clientid()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/GetTemplates/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.GetTemplatesCount(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void UpdateTemplate_with_minimal_parameters()
		{
			// Arrange
			var templateId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/SetTemplate/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "template_id" && (long)p.Value == templateId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateTemplate(USER_KEY, templateId, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateTemplate_with_labels()
		{
			// Arrange
			var templateId = 123;
			var labels = new Dictionary<string, string>()
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/SetTemplate/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "template_id" && (long)p.Value == templateId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[en_US]" && (string)p.Value == "My Category" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "label[fr_FR]" && (string)p.Value == "Ma Catégorie" && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateTemplate(USER_KEY, templateId, labels);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateTemplate_with_content()
		{
			// Arrange
			var templateId = 123;
			var content = "this is the content";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/SetTemplate/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "template_id" && (long)p.Value == templateId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "content" && (string)p.Value == content && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateTemplate(USER_KEY, templateId, null, content: content);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateTemplate_with_categoryid()
		{
			// Arrange
			var templateId = 123;
			var categoryId = 111;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/SetTemplate/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "template_id" && (long)p.Value == templateId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "category_id" && (long)p.Value == categoryId && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateTemplate(USER_KEY, templateId, null, categoryId: categoryId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void UpdateTemplate_with_clientid()
		{
			// Arrange
			var templateId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.Execute(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/TemplateV2/SetTemplate/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "template_id" && (long)p.Value == templateId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).Returns(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = apiClient.UpdateTemplate(USER_KEY, templateId, null, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}
	}
}