namespace BigTrack.Common.Database
{
	public interface IDialectDriver
	{
		string DatabaseTypeName { get; }
		string CatalogTypeName { get; }
		bool UserTrackingSupported { get; }
		IDatabaseManager DatabaseManager { get; }
	}
}