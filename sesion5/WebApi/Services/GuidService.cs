public class GuidService
{
	public Guid Id {get;}
	
	public GuidService()
	{
		Id= Guid.NewGuid();
	}
}