
#region Referencias

using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

#endregion

namespace DBE.Net.Facel.LibFacel.Clases
{
    internal class PasswordDigestBehavior : IEndpointBehavior
    {
        internal string Usuario { get; set; }
        internal string Password { get; set; }

        internal PasswordDigestBehavior(string username, string password)
        {
            this.Usuario = username;
            this.Password = password;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(new PasswordDigestMessageInspector(Usuario, Password));
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            return;
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            return;
        }
    }
}
