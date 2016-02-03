﻿using HalWithNancy.Services.Shared;

using Nancy;
using Nancy.Conventions;
using Nancy.Hal.Configuration;
using Nancy.TinyIoc;

namespace HalWithNancy {
	public class Bootstrapper : DefaultNancyBootstrapper {
		public Bootstrapper() {
			var x = 0;
		}
		protected override void ConfigureConventions(NancyConventions conventions) {
			base.ConfigureConventions(conventions);

			conventions.StaticContentsConventions.AddDirectory("/App");
			conventions.StaticContentsConventions.AddDirectory("/Fonts");
			conventions.StaticContentsConventions.AddDirectory("/Scripts");
		}

		protected override void ConfigureApplicationContainer(TinyIoCContainer container) {
			base.ConfigureApplicationContainer(container);

			container.Register(typeof(IProvideHalTypeConfiguration), HypermediaConfiguration());
		}

		private static HalConfiguration HypermediaConfiguration() {
			var config = new HalConfiguration();

			config.For<Services.Artists.ArtistPmo>()
				.Links(model => HomeModule.GetArtists.CreateLink(model));

			config.For<PagedList<Services.Artists.ArtistPmo>>()
				.Embeds("artists", (x) => x.Data)
				.Links(
					(model, ctx) => HomeModule.GetArtistsPaged.CreateLink("self", new { page = model.PageNumber, pageSize = model.PageSize }))
				.Links(
					(model, ctx) => HomeModule.GetArtistsPaged.CreateLink("prev", new { page = model.PageNumber - 1, pageSize = model.PageSize }),
					(model, ctx) => model.PageNumber > 1
				)
				.Links(
					(model, ctx) => HomeModule.GetArtistsPaged.CreateLink("next", new { page = model.PageNumber + 1, pageSize = model.PageSize }),
					(model, ctx) => model.PageNumber < model.TotalPages
				);

			return config;
		}
	}
}