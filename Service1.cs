using System;
using System.ServiceProcess;

namespace ImpressaoEtiquetasControlCenter
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                new BancoPostGres();
                Servidor.IniciarServidor();

            }
            catch(Exception Ex)
            {
                using (new Log(Ex.ToString())) ;
            }
          
        }

        protected override void OnStop()
        {
            using (new Log("Servidor Parado")) ;                
            this.Dispose();
        }
    }
}
