namespace CookingWeb.Services.Timing;

public interface ITimeProvider
{
	DateTime Now { get; }

	DateTime Today { get; }
}