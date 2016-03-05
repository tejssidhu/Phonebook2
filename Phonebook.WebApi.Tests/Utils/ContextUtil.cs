using Moq;
using Phonebook.WebApi.Tests.DependencyResolution;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

//http://aspnetwebstack.codeplex.com/SourceControl/changeset/view/98d041ae352f#test/System.Web.Http.Test/Util/ContextUtil.cs

namespace Phonebook.WebApi.Tests.Utils
{
	internal static class ContextUtil
	{
		public static HttpControllerContext CreateControllerContext(HttpConfiguration configuration = null, IHttpController instance = null, IHttpRouteData routeData = null, HttpRequestMessage request = null)
		{
			HttpConfiguration config = configuration ?? new HttpConfiguration();
			IHttpRouteData route = routeData ?? new HttpRouteData(new HttpRoute());
			HttpRequestMessage req = request ?? new HttpRequestMessage();
			req.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
			req.Properties[HttpPropertyKeys.HttpRouteDataKey] = route;
			req.RequestUri = new System.Uri("http://myurl");

			HttpControllerContext context = new HttpControllerContext(config, route, req);
			if (instance != null)
			{
				context.Controller = instance;
			}
			context.ControllerDescriptor = CreateControllerDescriptor(config);

			return context;
		}

		public static HttpActionContext CreateActionContext(HttpControllerContext controllerContext = null, HttpActionDescriptor actionDescriptor = null)
		{
			HttpControllerContext context = controllerContext ?? ContextUtil.CreateControllerContext();
			HttpActionDescriptor descriptor = actionDescriptor ?? new Mock<HttpActionDescriptor>() { CallBase = true }.Object;
			return new HttpActionContext(context, descriptor);
		}

		public static HttpActionContext GetHttpActionContext(HttpRequestMessage request)
		{
			HttpActionContext actionContext = CreateActionContext();
			actionContext.ControllerContext.Request = request;
			return actionContext;
		}

		public static HttpActionExecutedContext GetActionExecutedContext(HttpRequestMessage request, HttpResponseMessage response)
		{
			HttpActionContext actionContext = CreateActionContext();
			actionContext.ControllerContext.Request = request;
			HttpActionExecutedContext actionExecutedContext = new HttpActionExecutedContext(actionContext, null) { Response = response };
			return actionExecutedContext;
		}

		public static HttpControllerDescriptor CreateControllerDescriptor(HttpConfiguration config = null)
		{
			if (config == null)
			{
				config = new HttpConfiguration();
			}
			return new HttpControllerDescriptor();
		}
	}
}
