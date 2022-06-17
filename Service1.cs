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
                new Servidor();

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
