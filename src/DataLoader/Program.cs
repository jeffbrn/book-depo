using System;
using System.Threading;
using DataLoader.Application;

namespace DataLoader {
	class Program {
		static void Main(string[] args) {
			using var app = new DataLoaderApp(args);
			var cancel = new CancellationTokenSource();
			var wait = app.Run<Startup>(cancel.Token);
			wait.Wait(cancel.Token);
			Console.WriteLine("Exiting dataloader job");
		}
	}
}
