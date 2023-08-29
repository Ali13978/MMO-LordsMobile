using System.Collections;

namespace VoxelBusters.DesignPatterns
{
	public interface IObserver
	{
		void OnPropertyChange(string _key, ArrayList _data);
	}
}
