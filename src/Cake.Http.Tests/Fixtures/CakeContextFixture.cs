using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using NSubstitute;
using System;
using System.Reflection;
using System.Runtime.Versioning;

namespace Cake.Http.Tests.Fixtures
{
    public sealed class CakeContextFixture : ICakeContext, IDisposable
    {
        public IFileSystem FileSystem { get; set; }
        public ICakeEnvironment Environment { get; set; }
        public IGlobber Globber { get; set; }
        public ICakeLog Log { get; set; }
        public ICakeArguments Arguments { get; set; }
        public IProcessRunner ProcessRunner { get; set; }
        public IRegistry Registry { get; set; }
        public IToolLocator Tools { get; set; }
        public ICakeDataResolver Data { get; set; }

        public CakeContextFixture()
        {
            var cakeRuntime = Substitute.For<ICakeRuntime>();
            cakeRuntime.BuiltFramework.Returns(new FrameworkName(".NET Framework", new Version(4, 5, 2)));
            cakeRuntime.CakeVersion.Returns(typeof(ICakeRuntime).GetTypeInfo().Assembly.GetName().Version);
            
            FileSystem = Substitute.For<IFileSystem>();
            Environment = Substitute.For<ICakeEnvironment>();
            Environment.Runtime.Returns(cakeRuntime);

            Globber = Substitute.For<IGlobber>();
            Log = Substitute.For<ICakeLog>();
            Arguments = Substitute.For<ICakeArguments>();
            ProcessRunner = Substitute.For<IProcessRunner>();
            Registry = Substitute.For<IRegistry>();
            Tools = Substitute.For<IToolLocator>();
            Data = Substitute.For<ICakeDataResolver>();
        }

        public void Dispose()
        { }
    }
}
