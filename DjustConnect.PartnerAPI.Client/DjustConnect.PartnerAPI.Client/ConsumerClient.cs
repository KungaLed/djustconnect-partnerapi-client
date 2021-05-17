﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using DjustConnect.PartnerAPI.Client.Interfaces;
using DjustConnect.PartnerAPI.Client.DTOs;
using DjustConnect.PartnerAPI.Client.Filters;

namespace DjustConnect.PartnerAPI.Client
{

    //TODO

    //api/ConsumerAccess - GET
    //api/ConsumerAccess - POST

    //api/Consumer/push - GET
    //api/Consumer/push/activate - POST
    //api/Consumer/push/deactivate - POST

    public class ConsumerClient : DjustConnectClient, IConsumerClient
    {
        #region Constructors
        public ConsumerClient(HttpClient httpClient) : base(httpClient)
        {
        }
        public ConsumerClient(string thumbprint, string subscriptionkey)
        {
            _httpClient = DjustConnectClient.CreateHttpClient(thumbprint, subscriptionkey);
        }
        #endregion

        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        public Task GetFarmMappingAsync() // api/FarmMapping 
        {
            return null; // returns farm mapping - 415 Unsupported Media Type
        }

        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        public Task<ConsumerAccessDTO> GetConsumerAccessAsync()
        {
            return GetConsumerAccessAsync(CancellationToken.None);
        }
        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<ConsumerAccessDTO> GetConsumerAccessAsync(CancellationToken cancellationToken)
        {
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/ConsumerAccess?");

            return await CallAPI<ConsumerAccessDTO>(urlBuilder_, GetResult<ConsumerAccessDTO>,cancellationToken);
        }

        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        public Task<FarmIdTypeDTO[]> GetFarmIdTypesAsync() // api/FarmIdType
        {
            return GetFarmIdTypesAsync(CancellationToken.None);
        }

        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<FarmIdTypeDTO[]> GetFarmIdTypesAsync(CancellationToken cancellationToken)
        {
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/FarmIdType");

            return await CallAPI<FarmIdTypeDTO[]>(urlBuilder_, GetResult<FarmIdTypeDTO[]>, cancellationToken);
            // return await CallPagedAPI<FarmIdTypeDTO>(urlBuilder_, cancellationToken); moet dit een PagedResult zijn?
        }

        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        public Task<ResourceDTO[]> GetResourcesAsync() // api/Resource
        {
            return GetResourcesAsync(CancellationToken.None);
        }

        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<ResourceDTO[]> GetResourcesAsync(CancellationToken cancellationToken) // api/Resource
        {
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/Resource");

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));


                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);


                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            try
                            {
                                var result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<ResourceDTO[]>(responseData_);
                                return result_;
                            }
                            catch (Exception exception_)
                            {
                                throw new DjustConnectException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
                            }
                        }
                        else if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new DjustConnectException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }
                        return null;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }


        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        public Task<ResourceHealthDTO[]> GetResourceHealthAsync(Guid? resourceId) // api/Consumer/resource-health
        {
            // returns the current health of the resource you have access to
            return GetResourceHealthAsync(resourceId, CancellationToken.None);
        }
        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<ResourceHealthDTO[]> GetResourceHealthAsync(Guid? resourceId, CancellationToken cancellationToken) // api/Consumer/resource-health - requestURL: https://partnerapi.acc.djustconnect.cegeka.com/api/Consumer/resource-health
        {
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/Consumer/resource-health?");
            urlBuilder_.Append("ResourceId=").Append(Uri.EscapeDataString(resourceId != null ? ConvertToString(resourceId, System.Globalization.CultureInfo.InvariantCulture) : "")).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));


                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);


                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            try
                            {
                                var result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<ResourceHealthDTO[]>(responseData_);
                                return result_;
                            }
                            catch (Exception exception_)
                            {
                                throw new DjustConnectException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
                            }
                        }
                        else if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new DjustConnectException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }
                        return null;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        public Task<PagedResult<RarStatusDTO>> GetRarStatusAsyncWithFilter(RarStatusFilter filter)
        {
            return GetRarStatusAsyncWithFilter(filter, CancellationToken.None);
        }

        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<PagedResult<RarStatusDTO>> GetRarStatusAsyncWithFilter(RarStatusFilter filter, CancellationToken cancellationToken)
        {
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/RarStatus?");
            UrlAppend(urlBuilder_, "resourceNameFilter", filter.ResourceName);
            UrlAppend(urlBuilder_, "statusFilter", filter.Status);
            UrlAppend(urlBuilder_, "apiNameFilter", filter.ApiName);
            urlBuilder_.Length--;

            return await CallPagedAPI<RarStatusDTO>(urlBuilder_, cancellationToken);
        }

        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        public Task<PagedResult<DarStatusDTO>> GetDarStatusAsyncWithFilter(DarStatusFilter filter)
        {
            return GetDarStatusAsyncWithFilter(filter, CancellationToken.None);
        }

        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<PagedResult<DarStatusDTO>> GetDarStatusAsyncWithFilter(DarStatusFilter filter, CancellationToken cancellationToken)
        {
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/DarStatus?");
            UrlAppend(urlBuilder_, "farmNumberFilter", filter.FarmNumber);
            UrlAppend(urlBuilder_, "resourceNameFilter", filter.ResourceName);
            UrlAppend(urlBuilder_, "resourceIdFilter", filter.ResourceId.ToString());
            UrlAppend(urlBuilder_, "farmStatusFilter", filter.FarmStatus);
            UrlAppend(urlBuilder_, "resourceStatusFilter", filter.ResourceStatus);
            UrlAppend(urlBuilder_, "darStatusFilter", filter.DarStatus);
            urlBuilder_.Length--;

            return await CallPagedAPI<DarStatusDTO>(urlBuilder_, cancellationToken);
        }

        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        public Task<PagedResult<RarStatusDTO>> GetRarStatusAsync(string resourceNameFilter)
        {
            return GetRarStatusAsync(resourceNameFilter, CancellationToken.None);
        }

        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<PagedResult<RarStatusDTO>> GetRarStatusAsync(string resourceNameFilter, CancellationToken cancellationToken)
        {
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/RarStatus?");
            urlBuilder_.Append("resourceNameFilter=").Append(Uri.EscapeDataString(resourceNameFilter != null ? ConvertToString(resourceNameFilter, System.Globalization.CultureInfo.InvariantCulture) : "")).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));


                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);


                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }



                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            try
                            {

                                var result_ = new PagedResult<RarStatusDTO>
                                {
                                    PageNumber = headers_["X-PageNumber"].Select(x => Convert.ToInt32(x)).Single(),
                                    Pages = headers_["X-Pages"].Select(x => Convert.ToInt32(x)).Single(),
                                    PageSize = headers_["X-PageSize"].Select(x => Convert.ToInt32(x)).Single(),
                                    TotalCount = headers_["X-TotalCount"].Select(x => Convert.ToInt32(x)).Single(),
                                    Result = Newtonsoft.Json.JsonConvert.DeserializeObject<RarStatusDTO[]>(responseData_)
                                };
                                return result_;
                            }
                            catch (Exception exception_)
                            {
                                throw new DjustConnectException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
                            }
                        }
                        else if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new DjustConnectException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }
                        return null;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        public Task<PagedResult<DarStatusDTO>> GetDarStatusAsync(string farmNumberFilter)
        {
            return GetDarStatusAsync(farmNumberFilter, CancellationToken.None);
        }

        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<PagedResult<DarStatusDTO>> GetDarStatusAsync(string farmNumberFilter, CancellationToken cancellationToken)
        {
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/DarStatus?PageSize=100&");
            urlBuilder_.Append("FarmNumberFilter=").Append(Uri.EscapeDataString(farmNumberFilter != null ? ConvertToString(farmNumberFilter, System.Globalization.CultureInfo.InvariantCulture) : "")).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));


                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);


                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }



                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            try
                            {

                                var result_ = new PagedResult<DarStatusDTO> 
                                {
                                    PageNumber = headers_["X-PageNumber"].Select(x => Convert.ToInt32(x)).Single(),
                                    Pages = headers_["X-Pages"].Select(x => Convert.ToInt32(x)).Single(),
                                    PageSize = headers_["X-PageSize"].Select(x => Convert.ToInt32(x)).Single(),
                                    TotalCount = headers_["X-TotalCount"].Select(x => Convert.ToInt32(x)).Single(),
                                    Result = Newtonsoft.Json.JsonConvert.DeserializeObject<DarStatusDTO[]>(responseData_)
                                };
                                return result_;
                            }
                            catch (Exception exception_)
                            {
                                throw new DjustConnectException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
                            }
                        }
                        else if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new DjustConnectException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }
                        return null;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        public Task<PagedResult<FarmStatusDTO>> GetFarmStatusAsyncWithFilter(FarmStatusFilter filter)
        {
            return GetFarmStatusAsyncWithFilter(filter, CancellationToken.None);
        }

        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<PagedResult<FarmStatusDTO>> GetFarmStatusAsyncWithFilter(FarmStatusFilter filter, CancellationToken cancellationToken)
        {
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/FarmStatus?");
            UrlAppend(urlBuilder_, "farmNumberFilter", filter.FarmNumber);
            UrlAppend(urlBuilder_, "statusFilter", filter.Status.ToString());
            // TODO Add Sort
            urlBuilder_.Length--;

            return await CallPagedAPI<FarmStatusDTO>(urlBuilder_, cancellationToken);
        }

        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        public Task<PagedResult<FarmStatusDTO>> GetFarmStatusAsync(string farmNumberFilter)
        {
            return GetFarmStatusAsync(farmNumberFilter, CancellationToken.None);
        }

        /// <exception cref="DjustConnectException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<PagedResult<FarmStatusDTO>> GetFarmStatusAsync(string farmNumberFilter, CancellationToken cancellationToken)
        {
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/FarmStatus?");
            urlBuilder_.Append("FarmNumberFilter=").Append(Uri.EscapeDataString(farmNumberFilter != null ? ConvertToString(farmNumberFilter, System.Globalization.CultureInfo.InvariantCulture) : "")).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            try
                            {
                                var result_ = new PagedResult<FarmStatusDTO>
                                {
                                    PageNumber = headers_["X-PageNumber"].Select(x => Convert.ToInt32(x)).Single(),
                                    Pages = headers_["X-Pages"].Select(x => Convert.ToInt32(x)).Single(),
                                    PageSize = headers_["X-PageSize"].Select(x => Convert.ToInt32(x)).Single(),
                                    TotalCount = headers_["X-TotalCount"].Select(x => Convert.ToInt32(x)).Single(),
                                    Result = Newtonsoft.Json.JsonConvert.DeserializeObject<FarmStatusDTO[]>(responseData_)
                                };
                                return result_;
                            }
                            catch (Exception exception_)
                            {
                                throw new DjustConnectException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
                            }
                        }
                        else if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new DjustConnectException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }
                        return null;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        #region Helpers
        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is Enum)
            {
                string name = Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                            as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value;
                        }
                    }
                }
            }
            else if (value is bool)
            {
                return Convert.ToString(value, cultureInfo).ToLowerInvariant();
            }
            else if (value is byte[])
            {
                return Convert.ToBase64String((byte[])value);
            }
            else if (value != null && value.GetType().IsArray)
            {
                var array = Enumerable.OfType<object>((Array)value);
                return string.Join(",", Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }

            return Convert.ToString(value, cultureInfo);
        }

        public void UrlAppendPaging(StringBuilder urlBuilder, PagingFilter filter)
        {
            if (filter.PageSize != null)
                urlBuilder.Append($"PageSize=").Append(Uri.EscapeDataString(ConvertToString(filter.PageSize.Value, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
        }

        public void UrlAppend(StringBuilder urlBuilder, string parameterName, string parameter)
        {
            if (!string.IsNullOrEmpty(parameter))
                urlBuilder.Append($"{parameterName}=").Append(Uri.EscapeDataString(parameter)).Append("&");
        }
        #endregion
    }
}
