using System.Web.Mvc;

namespace carOnline.Areas.Administrativo
{
    public class AdministrativoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Administrativo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Administrativo_default",
                "Administrativo/{controller}/{action}/{id}",
                new { controller="Login", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "AdministrativoBuscarCidade",
                "Administrativo/{controller}/{action}/{id}",
                new { area="Administrativo", controller="Funcionario", action = "BuscarCidade", id = UrlParameter.Optional }
            );
        }
    }
}
