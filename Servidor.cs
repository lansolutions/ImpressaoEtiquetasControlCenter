using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SuperSimpleTcp;

namespace ImpressaoEtiquetasControlCenter
{
    public class Servidor : IDisposable
    {
        static SimpleTcpServer servidor;
        private bool disposedValue;

        public Servidor()
        {
            try
            {
                IniciarServidor();
            }
            catch(Exception Ex)
            {
                throw Ex;
            }
             
        }

        public void IniciarServidor()
        {
            try
            {
                if (servidor == null)
                {
                    servidor = new SimpleTcpServer(Ip());
                    servidor.Events.DataReceived += RecebimentoDeDados;
                    servidor.Start(); using (new Log("Servidor Iniciado")) ;
                }
            }
            catch (Exception Ex)
            {
                servidor.Dispose();
                throw Ex;
            }

        }
        
        private void RecebimentoDeDados(object sender, DataReceivedEventArgs e)
        {
            try
            {
                string Dados = Encoding.UTF8.GetString(e.Data);

                if (Dados.Contains("ImagemEtiqueta"))
                {
                    using (new Impressao(Dados));
                    
                }
            }
            catch(Exception Ex)
            {
                throw Ex;
            }
        }

        
        private string Ip()
        {
            string IP = string.Empty;
            try
            {
                string localIP = string.Empty;

                using (System.Net.Sockets.Socket socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;

                    localIP = endPoint.Address.ToString();
                    localIP = endPoint.Address.ToString();

                    IP = localIP;

                }
            }
            catch (Exception)
            {
                try
                {
                    string nomeMaquina = Dns.GetHostName();
                    IPAddress[] ipLocal = Dns.GetHostAddresses(nomeMaquina);
                    IP = ipLocal.LastOrDefault().ToString();
                }
                catch(Exception Ex)
                {
                    throw Ex;
                }
               
            }

            IP += ":3900";
            return IP;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Servidor()
        // {
        //     // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
