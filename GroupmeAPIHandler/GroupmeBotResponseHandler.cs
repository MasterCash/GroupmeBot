using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GroupmeAPIHandler.Models;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GroupmeAPIHandler.Services
{
    public class GroupmeBotResponseHandler
    {
        private readonly Dictionary<string, string> _token;
        private readonly HttpClient _client;
        public GroupmeBotResponseHandler(HttpClient client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            client.BaseAddress = new Uri("https://api.groupme.com/v3/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client = client;
            _token = new Dictionary<string, string>();
        }

        public void Init(string token)
        {
            _token.Add("token", token);
        }

        #region Groups

        public async Task<List<GroupResponse>> GetGroups(GroupsRequest request)
        {
            var queryParams = new Dictionary<string, string>(_token);
            if (request != null)
            {
                if (request.Omit != null)
                    queryParams.Add("omit", request.Omit);
                if (request.Page != null)
                    queryParams.Add("page", request.Page.ToString());
                if (request.PerPage != null)
                    queryParams.Add("per_page", request.PerPage.ToString());
            }


            var response = await _client.GetAsync(QueryHelpers.AddQueryString("groups", queryParams));
            response.EnsureSuccessStatusCode();
            var item = await response.Content.ReadAsAsync<GroupmeResponse>();
            return ((JArray)item.Response).ToObject<List<GroupResponse>>();
        }

        public async Task<List<GroupResponse>> GetFormerGroups()
        {
            var queryParams = new Dictionary<string, string>(_token);

            var response = await _client.GetAsync(QueryHelpers.AddQueryString("groups/former", queryParams));
            response.EnsureSuccessStatusCode();
            var item = await response.Content.ReadAsAsync<GroupmeResponse>();
            return ((JArray)item.Response).ToObject<List<GroupResponse>>();
        }

        public async Task<GroupResponse> GetGroup(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            var response = await _client.GetAsync(QueryHelpers.AddQueryString($"groups/{id}", _token));
            response.EnsureSuccessStatusCode();
            var item = await response.Content.ReadAsAsync<GroupmeResponse>();
            return ((JObject)item.Response).ToObject<GroupResponse>();
        }

        public async Task<GroupResponse> CreateGroup(GroupCreateRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var response = await _client.PostAsJsonAsync(QueryHelpers.AddQueryString("groups", _token), request);
            response.EnsureSuccessStatusCode();
            var item = await response.Content.ReadAsAsync<GroupmeResponse>();
            return ((JObject)item.Response).ToObject<GroupResponse>();
        }

        public async Task<GroupResponse> UpdateGroup(GroupUpdateRequest request, string groupId)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (groupId == null) throw new ArgumentNullException(nameof(groupId));

            var response = await _client.PostAsJsonAsync(QueryHelpers.AddQueryString($"groups/{groupId}/update", _token),
                request);
            response.EnsureSuccessStatusCode();
            var item = await response.Content.ReadAsAsync<GroupmeResponse>();
            return ((JObject)item.Response).ToObject<GroupResponse>();
        }

        public async Task DestroyGroup(string groupId)
        {
            if (groupId == null) throw new ArgumentNullException(nameof(groupId));

            var response = await _client.PostAsync(QueryHelpers.AddQueryString($"groups/{groupId}/destroy", _token), null);
            response.EnsureSuccessStatusCode();
        }

        public async Task<GroupResponse> JoinGroup(string groupId, string shareToken)
        {
            if (groupId == null) throw new ArgumentNullException(nameof(groupId));
            if (shareToken == null) throw new ArgumentNullException(nameof(shareToken));

            var response = await _client.PostAsync(QueryHelpers.AddQueryString($"groups/{groupId}/join/", _token), null);
            response.EnsureSuccessStatusCode();
            var item = await  response.Content.ReadAsAsync<GroupmeResponse>();
            return ((JObject)item.Response).ToObject<GroupResponse>();
        }

        public async Task<GroupResponse> RejoinGroup(string groupId)
        {
            if (groupId == null) throw new ArgumentNullException(nameof(groupId));
            var queryParams = new Dictionary<string,string>(_token)
            {
                {"group_id", groupId }
            };
            var response = await _client.PostAsync(QueryHelpers.AddQueryString($"group/join/", queryParams), null);
            response.EnsureSuccessStatusCode();
            var item = await response.Content.ReadAsAsync<GroupmeResponse>();
            return ((JObject)item.Response).ToObject<GroupResponse>();
        }

        /// <summary>
        /// status meaning:
        /// 200 - ok
        /// 400 - when requester is also a new owner
        /// 403 - requester is not owner of group
        /// 404 - group or new owner not found or new owner is not member of the group
        /// 405 - request object is missing required field or any of the required fields is not an ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ChangeOwnerResponse> ChangeOwners(ChangeOwnerRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var response =
                await _client.PostAsJsonAsync(QueryHelpers.AddQueryString("groups/change_owners", _token), request);
            response.EnsureSuccessStatusCode();
            var item = await response.Content.ReadAsAsync<GroupmeResponse>();
            return ((JObject)item.Response).ToObject<ChangeOwnerResponse>();
        }
        #endregion

        #region Members

        public async Task<MemberAddResponse> AddMembers(string groupId, MemberAddRequest request)
        {
            if (groupId == null) throw new ArgumentNullException(nameof(groupId));
            if (request == null) throw new ArgumentNullException(nameof(request));

            var response =
                await _client.PostAsJsonAsync(QueryHelpers.AddQueryString($"groups/{groupId}/members/add", _token),
                    request);
            response.EnsureSuccessStatusCode();
            var item = await response.Content.ReadAsAsync<GroupmeResponse>();
            return JsonConvert.DeserializeObject<MemberAddResponse>(JsonConvert.SerializeObject(item.Response));
        }

        public async Task<ResultsResponse> AddMemberResults(string groupId, string resultsId)
        {
            if (groupId == null) throw new ArgumentNullException(nameof(groupId));
            if (resultsId == null) throw new ArgumentNullException(nameof(resultsId));
            var response =
                await _client.GetAsync(QueryHelpers.AddQueryString($"groups/{groupId}/members/{resultsId}", _token));
            var item = await response.Content.ReadAsAsync<GroupmeResponse>();

            return JsonConvert.DeserializeObject<ResultsResponse>(JsonConvert.SerializeObject(item.Response));
        }

        public async Task RemoveMember(string groupId, string membershipId)
        {
            if (groupId == null) throw new ArgumentNullException(nameof(groupId));
            if (membershipId == null) throw new ArgumentNullException(nameof(membershipId));
            var response =
                await _client.PostAsync(QueryHelpers.AddQueryString($"groups/{groupId}/members/{membershipId}/remove", _token),
                    null);
            response.EnsureSuccessStatusCode();
        }

        public async Task<UpdateMemberResponse> UpdateMemberNickname(string groupId, UpdateMemberRequest request)
        {
            if (groupId == null) throw new ArgumentNullException(nameof(groupId));
            if (request == null) throw new ArgumentNullException(nameof(request));

            var response =
                await _client.PostAsJsonAsync(QueryHelpers.AddQueryString($"groups/{groupId}/memberships/update", _token),
                    request);
            response.EnsureSuccessStatusCode();

            var item = await response.Content.ReadAsAsync<GroupmeResponse>();
            return JsonConvert.DeserializeObject<UpdateMemberResponse>(JsonConvert.SerializeObject(item.Response));
        }
        #endregion

        #region Messages

        public async Task<JObject> Index2Messages(string groupId, MessageIndexRequest request)
        {
            if (groupId == null) throw new ArgumentNullException(nameof(groupId));
            var queryParams = new Dictionary<string, string>(_token);
            if (request != null)
            {
                if (!string.IsNullOrEmpty(request.AfterId))
                    queryParams.Add("after_id", request.AfterId);
                if (!string.IsNullOrEmpty(request.BeforeId))
                    queryParams.Add("before_id", request.BeforeId);
                if (!string.IsNullOrEmpty(request.SinceId))
                    queryParams.Add("since_id", request.SinceId);
                if (request.Limit != null)
                    queryParams.Add("limit", request.Limit.ToString());
            }

            var response =
                await _client.GetAsync(QueryHelpers.AddQueryString($"groups/{groupId}/messages", queryParams));
            if (response.IsSuccessStatusCode)
            {
                var item = await response.Content.ReadAsAsync<GroupmeResponse>();
                return ((JObject)item.Response);
            }
            else if (response.StatusCode == HttpStatusCode.NotModified)
            {
                return null;
            }
            else
            {
                response.EnsureSuccessStatusCode();
                throw new HttpRequestException();
            }
        }


        public async Task<MessageIndexResponse> IndexMessages(string groupId, MessageIndexRequest request)
        {
            if (groupId == null) throw new ArgumentNullException(nameof(groupId));
            var queryParams = new Dictionary<string,string>(_token);
            if (request != null)
            {
                if (!string.IsNullOrEmpty(request.AfterId))
                    queryParams.Add("after_id", request.AfterId);
                if (!string.IsNullOrEmpty(request.BeforeId))
                    queryParams.Add("before_id", request.BeforeId);
                if (!string.IsNullOrEmpty(request.SinceId))
                    queryParams.Add("since_id", request.SinceId);
                if (request.Limit != null)
                    queryParams.Add("limit", request.Limit.ToString());
            }

            var response =
                await _client.GetAsync(QueryHelpers.AddQueryString($"groups/{groupId}/messages", queryParams));
            if (response.IsSuccessStatusCode)
            {
                var item = await response.Content.ReadAsAsync<GroupmeResponse>();
                return ((JObject)item.Response).ToObject<MessageIndexResponse>();
            }
            else if (response.StatusCode == HttpStatusCode.NotModified)
            {
                return  new MessageIndexResponse()
                {
                    Messages = new List<GroupmeMessage>(),
                    Count = 0,
                };
            }
            else
            {
                response.EnsureSuccessStatusCode();
                throw new HttpRequestException();
            }
        }

        public async Task<GroupmeMessage> CreateMessage(string groupId, MessageCreateRequest request)
        {
            if (groupId == null) throw new ArgumentNullException(nameof(groupId));
            if (request == null) throw new ArgumentNullException(nameof(request));
            var response =
                await _client.PostAsJsonAsync(QueryHelpers.AddQueryString($"groups/{groupId}/messages", _token),
                    request);
            response.EnsureSuccessStatusCode();
            var item = await response.Content.ReadAsAsync<GroupmeResponse>();

            return ((JObject)item.Response).ToObject<GroupmeMessage>();
        }
        

        #endregion

        #region Bots
        public async Task<BotResponse> CreateBot(BotRequest request)
        {
            var _ = request ?? throw new ArgumentNullException(nameof(request));
            var response = await _client.PostAsJsonAsync(QueryHelpers.AddQueryString("bots", _token), request);
            response.EnsureSuccessStatusCode();
            var item = await response.Content.ReadAsAsync<GroupmeResponse>();
            return ((JObject) item.Response).ToObject<BotResponse>();
        }

        public async Task SendMessageAsBot(BotMessageRequest request)
        {
            var _ = request ?? throw new ArgumentNullException(nameof(request));
            var response = await _client.PostAsJsonAsync(QueryHelpers.AddQueryString("bots/post", _token), request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<BotResponse>> ListBots()
        {
            var response = await _client.GetAsync(QueryHelpers.AddQueryString("bots", _token));
            response.EnsureSuccessStatusCode();
            var item = await response.Content.ReadAsAsync<GroupmeResponse>();
            return ((JArray) item.Response).ToObject<List<BotResponse>>();
        }
        #endregion
    }
}
