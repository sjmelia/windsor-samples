namespace WindsorTest
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var container = new WindsorContainer();

            container.Register(Component.For<IMyDependency>().ImplementedBy<MyDependencyA>().Named("a"));
            container.Register(Component.For<IMyDependency>().ImplementedBy<MyDependencyB>().Named("b"));
            container.Register(Component.For<Consumer>()
                .DependsOn(ServiceOverride.ForKey("a").Eq("a"))
                .DependsOn(ServiceOverride.ForKey("b").Eq("b")));

            Console.WriteLine(container.Resolve<IMyDependency>("a").GetString);
            Console.WriteLine(container.Resolve<IMyDependency>("b").GetString);
            container.Resolve<Consumer>().Write();
            Console.ReadKey();
        }
    }

    interface IMyDependency
    {
        string GetString { get; }
    }

    class MyDependencyA : IMyDependency
    {
        public string GetString {  get { return "A"; } }
    }

    class MyDependencyB : IMyDependency
    {
        public string GetString { get { return "B"; } }
    }

    class Consumer
    {
        private IMyDependency a;

        private IMyDependency b;

        public Consumer(IMyDependency a, IMyDependency b)
        {
            this.a = a;
            this.b = b;
        }

        public void Write()
        { 
            Console.WriteLine(a.GetString);
            Console.WriteLine(b.GetString);
        }
    }
}
