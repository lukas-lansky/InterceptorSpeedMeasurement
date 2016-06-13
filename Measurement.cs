using Castle.MicroKernel.Registration;
using System.Linq;
using Castle.Windsor;
using BenchmarkDotNet.Attributes;
using Castle.DynamicProxy;
using System.Runtime.CompilerServices;
using System;

namespace InterceptorSpeedMeasurement
{
    [Config(typeof(MeasurementConfig))]
    public class Measurement
    {
        private readonly IService _thousandInterceptors;
        private readonly IService _hundredInterceptors;
        private readonly IService _tenInterceptors;
        private readonly IService _oneInterceptor;

        private readonly IService _thousandSingletonInterceptors;
        private readonly IService _hundredSingletonInterceptors;
        private readonly IService _tenSingletonInterceptors;
        private readonly IService _oneSingletonInterceptor;

        private readonly IService _tenSlowInterceptors;

        private readonly IService _noInterceptor;

        private readonly WindsorContainer _container;
        
        public Measurement()
        {
            _container = new WindsorContainer();
            _container.Register(
                
                Component.For<PassingInterceptor>().LifestyleTransient(),
                Component.For<WaitingInterceptor>().LifestyleTransient(),

                GetPassingRegistration(nameof(_thousandInterceptors), 1000),
                GetPassingRegistration(nameof(_hundredInterceptors), 100),
                GetPassingRegistration(nameof(_tenInterceptors), 10),
                GetPassingRegistration(nameof(_oneInterceptor), 1),

                GetPassingRegistration(nameof(_thousandSingletonInterceptors), 1000).LifestyleSingleton(),
                GetPassingRegistration(nameof(_hundredSingletonInterceptors), 100).LifestyleSingleton(),
                GetPassingRegistration(nameof(_tenSingletonInterceptors), 10).LifestyleSingleton(),
                GetPassingRegistration(nameof(_oneSingletonInterceptor), 1).LifestyleSingleton(),

                Component.For<IService>().ImplementedBy<Service>().Named(nameof(_noInterceptor)),

                Component.For<IService>().ImplementedBy<Service>().Named(nameof(_tenSlowInterceptors)).Interceptors(GetSlowInterceptors(10))
                );

            _thousandInterceptors = _container.Resolve<IService>(nameof(_thousandInterceptors));
            _hundredInterceptors = _container.Resolve<IService>(nameof(_hundredInterceptors));
            _tenInterceptors = _container.Resolve<IService>(nameof(_tenInterceptors));
            _oneInterceptor = _container.Resolve<IService>(nameof(_oneInterceptor));

            _thousandSingletonInterceptors = _container.Resolve<IService>(nameof(_thousandSingletonInterceptors));
            _hundredSingletonInterceptors = _container.Resolve<IService>(nameof(_hundredSingletonInterceptors));
            _tenSingletonInterceptors = _container.Resolve<IService>(nameof(_tenSingletonInterceptors));
            _oneSingletonInterceptor = _container.Resolve<IService>(nameof(_oneSingletonInterceptor));

            _tenSlowInterceptors = _container.Resolve<IService>(nameof(_tenSlowInterceptors));

            _noInterceptor = _container.Resolve<IService>(nameof(_noInterceptor));
        }

        private ComponentRegistration<IService> GetPassingRegistration(string name, int multiplicity)
            => Component.For<IService>().ImplementedBy<Service>().Named(name).Interceptors(GetPassingInterceptors(multiplicity));

        private Type[] GetPassingInterceptors(int multiplicity)
            => Enumerable.Range(0, multiplicity).Select(i => typeof(PassingInterceptor)).ToArray();

        private Type[] GetSlowInterceptors(int multiplicity)
            => Enumerable.Range(0, multiplicity).Select(i => typeof(WaitingInterceptor)).ToArray();

        [Benchmark]
        public int ThousandInterceptorsWithoutResolve() => _thousandInterceptors.Compute();

        [Benchmark]
        public int HundreadInterceptorsWithoutResolve() => _hundredInterceptors.Compute();

        [Benchmark]
        public int TenInterceptorsWithoutResolve() => _tenInterceptors.Compute();

        [Benchmark]
        public int OneInterceptorWithoutResolve() => _oneInterceptor.Compute();

        [Benchmark]
        public int ThousandInterceptorsWithResolve() => _container.Resolve<IService>(nameof(_thousandInterceptors)).Compute();

        [Benchmark]
        public int HundreadInterceptorsWithResolve() => _container.Resolve<IService>(nameof(_hundredInterceptors)).Compute();

        [Benchmark]
        public int TenInterceptorsWithResolve() => _container.Resolve<IService>(nameof(_tenInterceptors)).Compute();

        [Benchmark]
        public int OneInterceptorWithResolve() => _container.Resolve<IService>(nameof(_oneInterceptor)).Compute();
        
        [Benchmark]
        public int ThousandSingletonInterceptorsWithResolve() => _container.Resolve<IService>(nameof(_thousandSingletonInterceptors)).Compute();

        [Benchmark]
        public int HundreadSingletonInterceptorsWithResolve() => _container.Resolve<IService>(nameof(_hundredSingletonInterceptors)).Compute();

        [Benchmark]
        public int TenSingletonInterceptorsWithResolve() => _container.Resolve<IService>(nameof(_tenSingletonInterceptors)).Compute();

        [Benchmark]
        public int OneSingletonInterceptorWithResolve() => _container.Resolve<IService>(nameof(_oneSingletonInterceptor)).Compute();

        [Benchmark]
        public int TenSlowInterceptors() => _tenSlowInterceptors.Compute();

        [Benchmark]
        public int NoInterceptor() => _noInterceptor.Compute();
    }

    public interface IService
    {
        int Compute();
    }

    public class Service : IService
    {
        public int Compute() => 42;
    }

    public class PassingInterceptor : IInterceptor
    {
        [MethodImpl(MethodImplOptions.NoInlining)] // just to be sure
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }

    public class WaitingInterceptor : IInterceptor
    {
        [MethodImpl(MethodImplOptions.NoInlining)] // just to be sure
        public void Intercept(IInvocation invocation)
        {
            System.Threading.Thread.Sleep(5);

            invocation.Proceed();

            System.Threading.Thread.Sleep(5);
        }
    }
}
