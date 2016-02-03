using Nancy;
using Nancy.Hal;
using Nancy.ModelBinding;

namespace HalWithNancy {
	public class HomeModule : NancyModule {
		public static Link GetArtists = new Link("self", "/artists/{id}");
		public static Link GetArtistsPaged = new Link("artists", "/artists/{?query,page,pageSize}");

		public HomeModule(Services.IArtistService artistService) : base("/") {
			Get["/"] = _ => {
				return Negotiate.WithView("index");
			};
			Get["/artists"] = _ => {
				var criteria = this.Bind<Services.Artists.ArtistPagedCriteria>();
				return Negotiate.WithModel(artistService.GetPage(criteria));
			};
		}
	}
}