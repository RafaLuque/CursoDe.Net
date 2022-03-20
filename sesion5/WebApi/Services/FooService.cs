public class FooService{
	public string Id {get;}

	public FooService(GuidService svc)
	{
		Id = $"{Guid.NewGuid()}/{svc.Id}";
	}
}