using System.Collections.Generic;

namespace Template.Integration.Options
{
    public class ComunicacaoHttp
    {
        public List<Configuracoes> Configuracoes { get; set; }
    }

    public class Configuracoes
    {
        public string NomeConfiguracao { get; set; }
        public bool Inativo { get; set; }
        public bool IgnorarSsl { get; set; }
        public long Timeout { get; set; }
        public string CaminhoCertificado { get; set; }
        public string Url { get; set; }
    }
}
