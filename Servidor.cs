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
    public class Servidor
    {
        static SimpleTcpServer servidor;

        
        public static void IniciarServidor()
        {
            try
            {
                if (servidor == null)
                {
                    servidor = new SimpleTcpServer(Ip());                    
                }

                servidor.Events.DataReceived += RecebimentoDeDados;
                servidor.Start(); new Log("Servidor Iniciado");
            }
            catch (Exception Ex)
            {
                servidor.Dispose();
                using (new Log(Ex.ToString())) ;
            }

        }
        
        private static void RecebimentoDeDados(object sender, DataReceivedEventArgs e)
        {
            try
            {
                string Dados = Encoding.UTF8.GetString(e.Data);

                if (Dados.Contains("ImagemEtiqueta"))
                {       
                    Impressao.Imprimir(Dados);
                }
            }
            catch(Exception Ex)
            {
                using (new Log(Ex.ToString())) ;
            }
        }
        
        private static string Ip()
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
     
    }
}
