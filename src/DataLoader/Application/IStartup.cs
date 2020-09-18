using System.Threading;

namespace DataLoader.Application {
	public interface IStartup {
		void Run(CancellationToken cancel);
	}
}
