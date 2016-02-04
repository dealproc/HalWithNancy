﻿using Nancy;
using Nancy.Hal;
using Nancy.ModelBinding;

namespace HalWithNancy {
	public class HomeModule : NancyModule {
		public static Link GetArtists = new Link("self", "/artists/{id}", "Edit");
		public static Link GetArtistsPaged = new Link("artists", "/artists/{?page,pageSize,keywords,sortBy,sortByDir}", "List");

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