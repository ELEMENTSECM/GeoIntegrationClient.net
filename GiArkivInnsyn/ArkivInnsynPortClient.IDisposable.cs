using System;
using System.ServiceModel;

namespace ServiceReference
{
	// Make client adhere better to IDisposable. More info: https://coding.abel.nu/2012/02/using-and-disposing-of-wcf-clients/
	partial class ArkivInnsynPortClient : IDisposable
	{
		void IDisposable.Dispose()
		{
			Dispose(true);
		}

		private bool disposedValue = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					try
					{
						if (State != CommunicationState.Faulted)
						{
#if NETFRAMEWORK
							Close();
#else
							// For details see: https://github.com/dotnet/wcf/issues/3676
							((ICommunicationObject)this).Close();
#endif
						}
					}
					finally
					{
						if (State != CommunicationState.Closed)
						{
							Abort();
						}
					}
				}

				disposedValue = true;
			}
		}
	}
}
