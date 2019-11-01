using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace GeoIntegrationClient
{
	// Based on information found at https://stackoverflow.com/questions/14621544/how-can-i-add-authorization-header-to-the-request-in-wcf
	class AuthenticationHeaderBehavior : IEndpointBehavior
	{
		private readonly string _password;
		private readonly string _userName;

		public AuthenticationHeaderBehavior(string userName, string password)
		{
			_password = password;
			_userName = userName;
		}
		public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }

		public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			clientRuntime.ClientMessageInspectors.Add(new AuthenticationHeader(_userName, _password));
		}

		public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) { }

		public void Validate(ServiceEndpoint endpoint) { }
	}

	class AuthenticationHeader : IClientMessageInspector
	{
		private readonly string _password;
		private readonly string _userName;

		public AuthenticationHeader(string userName, string password)
		{
			_password = password;
			_userName = userName;
		}
		public void AfterReceiveReply(ref Message reply, object correlationState)
		{
			//Console.WriteLine("Received the following reply: '{0}'", reply.ToString());
		}

		public object BeforeSendRequest(ref Message request, IClientChannel channel)
		{
			HttpRequestMessageProperty messageProperty;
			if (request.Properties.ContainsKey("httpRequest")){
				messageProperty = (HttpRequestMessageProperty)request.Properties["httpRequest"];
			}
			else { 
				messageProperty = new HttpRequestMessageProperty();
				request.Properties.Add(HttpRequestMessageProperty.Name, messageProperty);
			}
			var encoded = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(_userName + ":" + _password));
			messageProperty.Headers.Add("Authorization", "Basic " + encoded);
			return request;
		}
	}
}
