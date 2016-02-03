using System;

namespace HalWithNancy.DataModels {
	public interface IEntity {
		int Id { get; set; }
	}
	public abstract class DataModelBase : IEntity {
		public virtual int Id { get; set; }
	}

	public class Album : DataModelBase {
		public string Title { get; set; }
		public int ArtistId { get; set; }
	}

	public class Artist : DataModelBase {
		[SQLite.PrimaryKey(), SQLite.Column("ArtistId")]
		public override int Id { get; set; }
		public string Name { get; set; }
	}

	public class Customer : DataModelBase {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Company { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public string PostalCode { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }
		public string Email { get; set; }
		public int SupportRepId { get; set; }
	}

	public class Employee : DataModelBase {
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string Title { get; set; }
		public int ReportsTo { get; set; }
		public DateTime BirthDate { get; set; }
		public DateTime HireDate { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public string PostalCode { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }
		public string Email { get; set; }
	}

	public class Genre : DataModelBase {
		public string Name { get; set; }
	}

	public class Invoice : DataModelBase {
		public int CustomerId { get; set; }
		public DateTime InvoiceDate { get; set; }
		public string BillingAddress { get; set; }
		public string BillingCity { get; set; }
		public string BillingState { get; set; }
		public string BillingCountry { get; set; }
		public string BillingPostalCode { get; set; }
		public decimal Total { get; set; }
	}

	public class InvoiceLine : DataModelBase {
		public int InvoiceId { get; set; }
		public int TrackId { get; set; }
		public decimal UnitPrice { get; set; }
		public int Quantity { get; set; }
	}

	public class MediaType : DataModelBase {
		public string Name { get; set; }
	}

	public class Playlist : DataModelBase {
		public string Name { get; set; }
	}

	public class PlaylistTrack {
		public int PlaylistId { get; set; }
		public int TrackId { get; set; }
	}

	public class Track : DataModelBase {
		public string Name { get; set; }
		public int AlbumId { get; set; }
		public int MediaTypeId { get; set; }
		public int GenreId { get; set; }
		public string Composer { get; set; }
		public int Milliseconds { get; set; }
		public int Bytes { get; set; }
		public decimal UnitPrice { get; set; }
	}
}