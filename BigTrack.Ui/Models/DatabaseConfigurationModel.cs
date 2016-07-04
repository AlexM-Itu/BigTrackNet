namespace BigTrack.Ui.Models
{
	public class DatabaseConfigurationModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string DatabaseTypeName { get; set; }
		public string CatalogTypeName { get; set; }
		public bool IsUserTrackingSupported { get; set; }
	}
}