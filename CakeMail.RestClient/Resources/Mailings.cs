﻿using CakeMail.RestClient.Models;
using CakeMail.RestClient.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public class Mailings
	{
		#region Fields

		private readonly CakeMailRestClient _cakeMailRestClient;

		#endregion

		#region Constructor

		public Mailings(CakeMailRestClient cakeMailRestClient)
		{
			_cakeMailRestClient = cakeMailRestClient;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Create a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="name">Name of the mailing.</param>
		/// <param name="campaignId">ID of the campaign you want to associate the mailing with.</param>
		/// <param name="type">Type of the mailing. Possible values: 'standard', 'recurring', 'absplit'</param>
		/// <param name="recurringId">ID of the first recurrence in case of a 'recurring' or 'absplit' mailing.</param>
		/// <param name="encoding">Encoding to be used for the mailing. Possible values: 'utf-8', 'iso-8859-x'</param>
		/// <param name="transferEncoding">Transfer encoding to be used for the mailing. Possible values: 'quoted-printable', 'base64'</param>
		/// <param name="clientId">Client ID of the client in which the mailing is created.</param>
		/// <returns>ID of the new mailing</returns>
		public async Task<long> CreateAsync(string userKey, string name, long? campaignId = null, MailingType? type = MailingType.Standard, long? recurringId = null, MessageEncoding? encoding = null, TransferEncoding? transferEncoding = null, long? clientId = null)
		{
			string path = "/Mailing/Create/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("name", name)
			};
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (type.HasValue) parameters.Add(new KeyValuePair<string, object>("type", type.Value.GetEnumMemberValue()));
			if (recurringId.HasValue) parameters.Add(new KeyValuePair<string, object>("recurring_id", recurringId.Value));
			if (encoding.HasValue) parameters.Add(new KeyValuePair<string, object>("encoding", encoding.Value.GetEnumMemberValue()));
			if (transferEncoding.HasValue) parameters.Add(new KeyValuePair<string, object>("transfer_encoding", transferEncoding.Value.GetEnumMemberValue()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<long>(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Delete a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the mailing is deleted</returns>
		public async Task<bool> DeleteAsync(string userKey, long mailingId, long? clientId = null)
		{
			string path = "/Mailing/Delete/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId),
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The <see cref="Mailing">mailing</see></returns>
		public async Task<Mailing> GetAsync(string userKey, long mailingId, long? clientId = null)
		{
			var path = "/Mailing/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<Mailing>(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve the mailings matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the mailing status. Possible values: 'incomplete', 'scheduled', 'delivering', 'delivered'</param>
		/// <param name="type">Filter using the mailing type. Possible values: 'standard', 'recurring', 'absplit'</param>
		/// <param name="name">Filter using the mailing name.</param>
		/// <param name="listId">Filter using the ID of the mailing list.</param>
		/// <param name="campaignId">Filter using the ID of the mailing campaign.</param>
		/// <param name="recurringId">Filter using the ID of the mailing recurrence.</param>
		/// <param name="start">Filter using a start date.</param>
		/// <param name="end">Filter using a end date.</param>
		/// <param name="sortBy">Sort resulting mailings. Possible values: 'name', 'created_on', 'scheduled_for', 'scheduled_on', 'active_emails'</param>
		/// <param name="sortDirection">Direction of the sorting. Possible values: 'asc', 'desc'</param>
		/// <param name="limit">Limit the number of resulting mailings.</param>
		/// <param name="offset">Offset the beginning of resulting mailings.</param>
		/// <param name="clientId">Client ID of the client in which the mailings are located.</param>
		/// <returns>Enumeration of <see cref="Mailing">mailings</see> matching the filtering criteria</returns>
		public async Task<IEnumerable<Mailing>> GetMailingsAsync(string userKey, MailingStatus? status = null, MailingType? type = null, string name = null, long? listId = null, long? campaignId = null, long? recurringId = null, DateTime? start = null, DateTime? end = null, MailingsSortBy? sortBy = null, SortDirection? sortDirection = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			var path = "/Mailing/GetList/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (type.HasValue) parameters.Add(new KeyValuePair<string, object>("type", type.Value.GetEnumMemberValue()));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (listId.HasValue) parameters.Add(new KeyValuePair<string, object>("list_id", listId.Value));
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (recurringId.HasValue) parameters.Add(new KeyValuePair<string, object>("recurring_id", recurringId.Value));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_date", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_date", end.Value.ToCakeMailString()));
			if (sortBy.HasValue) parameters.Add(new KeyValuePair<string, object>("sort_by", sortBy.Value.GetEnumMemberValue()));
			if (sortDirection.HasValue) parameters.Add(new KeyValuePair<string, object>("direction", sortDirection.Value.GetEnumMemberValue()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteArrayRequestAsync<Mailing>(path, parameters, "mailings").ConfigureAwait(false);
		}

		/// <summary>
		/// Get a count of mailings matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the mailing status. Possible values: 'incomplete', 'scheduled', 'delivering', 'delivered'</param>
		/// <param name="type">Filter using the mailing type. Possible values: 'standard', 'recurring', 'absplit'</param>
		/// <param name="name">Filter using the mailing name.</param>
		/// <param name="listId">Filter using the ID of the mailing list.</param>
		/// <param name="campaignId">Filter using the ID of the mailing campaign.</param>
		/// <param name="recurringId">Filter using the ID of the mailing recurrence.</param>
		/// <param name="start">Filter using a start date.</param>
		/// <param name="end">Filter using a end date.</param>
		/// <param name="clientId">Client ID of the client in which the mailings are located.</param>
		/// <returns>The count of mailings matching the filtering criteria</returns>
		public async Task<long> GetCountAsync(string userKey, MailingStatus? status = null, MailingType? type = null, string name = null, long? listId = null, long? campaignId = null, long? recurringId = null, DateTime? start = null, DateTime? end = null, long? clientId = null)
		{
			var path = "/Mailing/GetList/";
			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (type.HasValue) parameters.Add(new KeyValuePair<string, object>("type", type.Value.GetEnumMemberValue()));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (listId.HasValue) parameters.Add(new KeyValuePair<string, object>("list_id", listId.Value));
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (recurringId.HasValue) parameters.Add(new KeyValuePair<string, object>("recurring_id", recurringId.Value));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_date", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_date", end.Value.ToCakeMailString()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteCountRequestAsync(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Update a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="campaignId">ID of the campaign you want to associate the mailing with</param>
		/// <param name="listId">ID of the list you want to associate the mailing with.</param>
		/// <param name="sublistId">ID of the segment you want to associate the mailing with.</param>
		/// <param name="name">Name of the mailing.</param>
		/// <param name="type">Type of the mailing. Possible values: 'standard', 'recurring', 'absplit'</param>
		/// <param name="encoding">Encoding to be used for the mailing. Possible values: 'utf-8', 'iso-8859-x'</param>
		/// <param name="transferEncoding">Transfer encoding to be used for the mailing. Possible values: 'quoted-printable', 'base64'</param>
		/// <param name="subject">Subject of the mailing.</param>
		/// <param name="senderEmail">Email address of the sender of the mailing.</param>
		/// <param name="senderName">Name of the sender of the mailing.</param>
		/// <param name="replyTo">Email address of the reply-to of the mailing.</param>
		/// <param name="htmlContent">HTML content of the mailing.</param>
		/// <param name="textContent">Text content of the mailing.</param>
		/// <param name="trackOpens">Indicates if opens in the HTML version are tracked.</param>
		/// <param name="trackClicksInHtml">Indicates if clicks in the HTML version are tracked.</param>
		/// <param name="trackClicksInText">Indicates if clicks in the text version are tracked.</param>
		/// <param name="trackingParameters">Additional tracking parameters for links.</param>
		/// <param name="endingOn">The date to end a 'recurring' mailing.</param>
		/// <param name="maxRecurrences">The number of recurrences for a 'recurring' mailing.</param>
		/// <param name="recurringConditions">The recurring conditions for a 'recurring' mailing.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the mailing was updated</returns>
		public async Task<bool> UpdateAsync(string userKey, long mailingId, long? campaignId = null, long? listId = null, long? sublistId = null, string name = null, MailingType? type = null, MessageEncoding? encoding = null, TransferEncoding? transferEncoding = null, string subject = null, string senderEmail = null, string senderName = null, string replyTo = null, string htmlContent = null, string textContent = null, bool? trackOpens = null, bool? trackClicksInHtml = null, bool? trackClicksInText = null, string trackingParameters = null, DateTime? endingOn = null, int? maxRecurrences = null, string recurringConditions = null, long? clientId = null)
		{
			var path = "/Mailing/SetInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (listId.HasValue) parameters.Add(new KeyValuePair<string, object>("list_id", listId.Value));
			if (sublistId.HasValue) parameters.Add(new KeyValuePair<string, object>("sublist_id", sublistId.Value));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (type.HasValue) parameters.Add(new KeyValuePair<string, object>("type", type.Value.GetEnumMemberValue()));
			if (encoding.HasValue) parameters.Add(new KeyValuePair<string, object>("encoding", encoding.Value.GetEnumMemberValue()));
			if (transferEncoding.HasValue) parameters.Add(new KeyValuePair<string, object>("transfer_encoding", transferEncoding.Value.GetEnumMemberValue()));
			if (subject != null) parameters.Add(new KeyValuePair<string, object>("subject", subject));
			if (senderEmail != null) parameters.Add(new KeyValuePair<string, object>("sender_email", senderEmail));
			if (senderName != null) parameters.Add(new KeyValuePair<string, object>("sender_name", senderName));
			if (replyTo != null) parameters.Add(new KeyValuePair<string, object>("reply_to", replyTo));
			if (htmlContent != null) parameters.Add(new KeyValuePair<string, object>("html_message", htmlContent));
			if (textContent != null) parameters.Add(new KeyValuePair<string, object>("text_message", textContent));
			if (trackOpens.HasValue) parameters.Add(new KeyValuePair<string, object>("opening_stats", trackOpens.Value ? "true" : "false"));
			if (trackClicksInHtml.HasValue) parameters.Add(new KeyValuePair<string, object>("clickthru_html", trackClicksInHtml.Value ? "true" : "false"));
			if (trackClicksInText.HasValue) parameters.Add(new KeyValuePair<string, object>("clickthru_text", trackClicksInText.Value ? "true" : "false"));
			if (trackingParameters != null) parameters.Add(new KeyValuePair<string, object>("tracking_params", trackingParameters));
			if (endingOn.HasValue) parameters.Add(new KeyValuePair<string, object>("ending_on", endingOn.Value.ToCakeMailString()));
			if (maxRecurrences.HasValue) parameters.Add(new KeyValuePair<string, object>("max_recurrences", maxRecurrences.Value));
			if (recurringConditions != null) parameters.Add(new KeyValuePair<string, object>("recurring_conditions", recurringConditions));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Send a test of a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="recipientEmail">Email address where the test will be sent.</param>
		/// <param name="separated">True if you want the HTML and the text to be sent seperatly, false if you want to combine the HTML and the text in a multi-part email.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the test email was sent</returns>
		public async Task<bool> SendTestEmailAsync(string userKey, long mailingId, string recipientEmail, bool separated = false, long? clientId = null)
		{
			var path = "/Mailing/SendTestEmail/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId),
				new KeyValuePair<string, object>("test_email", recipientEmail),
				new KeyValuePair<string, object>("test_type", separated ? "separated" : "merged")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Get the multi-part version of a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The <see cref="RawEmailMessage">multi-part message</see></returns>
		public async Task<RawEmailMessage> GetRawEmailMessageAsync(string userKey, long mailingId, long? clientId = null)
		{
			var path = "/Mailing/GetEmailMessage/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<RawEmailMessage>(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Get the rendered HTML version of a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The rendered HTML</returns>
		public async Task<string> GetRawHtmlAsync(string userKey, long mailingId, long? clientId = null)
		{
			var path = "/Mailing/GetHtmlMessage/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<string>(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Get the rendered text version of a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The rendered text</returns>
		public async Task<string> GetRawTextAsync(string userKey, long mailingId, long? clientId = null)
		{
			var path = "/Mailing/GetTextMessage/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<string>(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Schedule a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="date">Date when the mailing is scheduled. If not provided, the mailing will be sent right away.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the mailing is scheduled</returns>
		public async Task<bool> ScheduleAsync(string userKey, long mailingId, DateTime? date = null, long? clientId = null)
		{
			var path = "/Mailing/Schedule/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (date.HasValue) parameters.Add(new KeyValuePair<string, object>("date", date.Value.ToCakeMailString()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Unschedule a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the mailing is unscheduled</returns>
		public async Task<bool> UnscheduleAsync(string userKey, long mailingId, long? clientId = null)
		{
			var path = "/Mailing/Unschedule/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Suspend a delivering mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the mailing is suspended</returns>
		public async Task<bool> SuspendAsync(string userKey, long mailingId, long? clientId = null)
		{
			var path = "/Mailing/Suspend/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Resume a suspended mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the mailing is resumed</returns>
		public async Task<bool> ResumeAsync(string userKey, long mailingId, long? clientId = null)
		{
			var path = "/Mailing/Resume/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve the log items for a given mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="logType">Filter using the log action. Possible values: "in_queue", "opened", "clickthru", "forward", "unsubscribe", "view", "spam", "skipped"</param>
		/// <param name="listMemberId">Filter using the ID of the member.</param>
		/// <param name="uniques">Return unique log item per member.</param>
		/// <param name="totals">Return all the log items.</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>An enumeration of <see cref="LogItem">log items</see> matching the filter criteria</returns>
		/// <remarks>
		/// CakeMail throws an exception if you attempt to retrieve the logs for a mailing that hasn't been sent. 
		/// The current error message is cryptic: Table 'api_cake_logs.mailing_xxxxxxx_big' doesn't exist.
		/// I was assured in May 2015 that they will improve this message to make it more informative.
		/// </remarks>
		public async Task<IEnumerable<LogItem>> GetLogsAsync(string userKey, long mailingId, LogType? logType = null, long? listMemberId = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			string path = "/Mailing/GetLog/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId),
				new KeyValuePair<string, object>("totals", totals ? "true" : "false"),
				new KeyValuePair<string, object>("uniques", uniques ? "true" : "false"),
				new KeyValuePair<string, object>("count", "false")
			};
			if (logType.HasValue) parameters.Add(new KeyValuePair<string, object>("action", logType.Value.GetEnumMemberValue()));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (listMemberId.HasValue) parameters.Add(new KeyValuePair<string, object>("record_id", listMemberId.Value));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteArrayRequestAsync<LogItem>(path, parameters, "logs").ConfigureAwait(false);
		}

		/// <summary>
		/// Get a count of log items matching the filter criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="logType">Filter using the log action. Possible values: "in_queue", "opened", "clickthru", "forward", "unsubscribe", "view", "spam", "skipped"</param>
		/// <param name="listMemberId">Filter using the ID of the member.</param>
		/// <param name="uniques">Return unique log item per member.</param>
		/// <param name="totals">Return all the log items.</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The number of log items matching the filtering criteria</returns>
		public async Task<long> GetLogsCountAsync(string userKey, long mailingId, LogType? logType = null, long? listMemberId = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, long? clientId = null)
		{
			string path = "/Mailing/GetLog/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId),
				new KeyValuePair<string, object>("totals", totals ? "true" : "false"),
				new KeyValuePair<string, object>("uniques", uniques ? "true" : "false"),
				new KeyValuePair<string, object>("count", "true")
			};
			if (logType.HasValue) parameters.Add(new KeyValuePair<string, object>("action", logType.Value.GetEnumMemberValue()));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (listMemberId.HasValue) parameters.Add(new KeyValuePair<string, object>("record_id", listMemberId.Value));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteCountRequestAsync(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve the links for a given mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>An enumeration of <see cref="Link">links</see> matching the filter criteria</returns>
		/// <remarks>
		/// The CakeMail API returns an empty array if you attempt to get the links in a mailing that has not been sent, even if the HTML contains multiple links.
		/// </remarks>
		public async Task<IEnumerable<Link>> GetLinksAsync(string userKey, long mailingId, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			string path = "/Mailing/GetLinks/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId),
				new KeyValuePair<string, object>("count", "false")
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteArrayRequestAsync<Link>(path, parameters, "links").ConfigureAwait(false);
		}

		/// <summary>
		/// Get a count of links matching the filter criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The number of links matching the filtering criteria</returns>
		public async Task<long> GetLinksCountAsync(string userKey, long mailingId, long? clientId = null)
		{
			string path = "/Mailing/GetLinks/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteCountRequestAsync(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve a link
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="linkId">ID of the link.</param>
		/// <param name="clientId">Client ID of the client in which the link is located.</param>
		/// <returns>The <see cref="Link">link</see></returns>
		public async Task<Link> GetLinkAsync(string userKey, long linkId, long? clientId = null)
		{
			string path = "/Mailing/GetLinkInfo/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("link_id", linkId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<Link>(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve the links (with their statistics) for a given mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting links.</param>
		/// <param name="offset">Offset the beginning of resulting links.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>An enumeration of <see cref="LinkStats">links with their statistics</see> matching the filter criteria</returns>
		public async Task<IEnumerable<LinkStats>> GetLinksWithStatsAsync(string userKey, long mailingId, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			string path = "/Mailing/GetLinksLog/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId),
				new KeyValuePair<string, object>("count", "false")
			};
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteArrayRequestAsync<LinkStats>(path, parameters, "links").ConfigureAwait(false);
		}

		/// <summary>
		/// Get a count of links (with their statistics) for a given mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting links.</param>
		/// <param name="offset">Offset the beginning of resulting links.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The number of links matching the filter criteria</returns>
		public async Task<long> GetLinksWithStatsCountAsync(string userKey, long mailingId, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			string path = "/Mailing/GetLinksLog/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("mailing_id", mailingId),
				new KeyValuePair<string, object>("count", "true")
			};
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteCountRequestAsync(path, parameters).ConfigureAwait(false);
		}

		#endregion
	}
}
