using HalWithNancy.Services.Shared;

using Nancy;
using Nancy.ModelBinding;
using Nancy.Hal;
using Nancy.Hal.Configuration;

namespace HalWithNancy.Modules {
	public class HomeModule : NancyModule {
		public static Link GetArtists = new Link("artist", "/artists/{id}");
		public static Link GetArtistsPaged = new Link("artists", "/artists/{?query,page,pageSize}");

		public HomeModule(Services.IArtistService artistService) : base("/") {
			Get["/"] = _ => {
				var model = artistService.GetPage(new Services.Artists.ArtistPagedCriteria() { PageSize = 50 });
				return Negotiate.WithModel(model).WithView("index");
			};
			Get["/artists"] = _ => {
				var criteria = this.Bind<Services.Artists.ArtistPagedCriteria>();

				Context.LocalHalConfigFor<Services.Artists.ArtistPmo>()
					.Links(model => GetArtists.CreateLink(model));

				Context.LocalHalConfigFor<IPagedList<Services.Artists.ArtistPmo>>()
					.Links((model, ctx) => GetArtistsPaged.CreateLink("self", new { page = model.PageNumber, pageSize = model.PageSize }))
					.Links((model, ctx) => GetArtistsPaged.CreateLink("prev", new { page = model.PageNumber - 1, pageSize = model.PageSize }))
					.Links((model, ctx) => GetArtistsPaged.CreateLink("next", new { page = model.PageNumber + 1, pageSize = model.PageSize }));

				return artistService.GetPage(criteria);
			};
		}
	}
}