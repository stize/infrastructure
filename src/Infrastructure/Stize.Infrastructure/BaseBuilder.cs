using System;
using Pulumi;
using Pulumi.Random;
using Stize.Infrastructure.Strategies;

namespace Stize.Infrastructure
{

    /// <summary>
    /// Base class for all the builders
    /// </summary>

    public abstract class BaseBuilder
    {
        /// <summary>
        /// Pulumi component name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Resource tags
        /// </summary>
        public InputMap<string> Tags { get; private set; } = new InputMap<string>();

        /// <summary>
        /// Resource context on which the object will be created on
        /// </summary>
        public ResourceContext Context { get; private set; }

        /// <summary>
        /// Resource location is is deployed to
        /// </summary>
        public Input<string> Location { get; set; }

        /// <summary>
        /// The resource strategy this builder should use to generate things like name or tags
        /// </summary>
        public IResourceStrategy ResourceStrategy { get; private set; }

        /// <summary>
        /// Pulumi Custom Resource Options
        /// </summary>
        public CustomResourceOptions CustomResourceOptions { get; private set; } = new CustomResourceOptions();

        /// <summary>
        /// Creates a new instance of <see cref="BaseBuilder"/>
        /// </summary>
        /// <param name="name">Pulumi internal name of the component</param>
        public BaseBuilder(string name) : this(name, new ResourceContext())
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="BaseBuilder"/>
        /// </summary>
        /// <param name="name">Pulumi internal name of the component</param>
        /// <param name="cro"></param>
        public BaseBuilder(string name, CustomResourceOptions cro) : this(name)
        {
            CustomResourceOptions = cro;
        }

        /// <summary>
        /// Creates a new instance of <see cref="BaseBuilder"/>
        /// </summary>
        /// <param name="name">Pulumi internal name of the component</param>
        /// <param name="context">Context generator this builder should use</param>
        /// <returns></returns>
        public BaseBuilder(string name, ResourceContext context)
        {
            Name = name;
            Context = context;
            ResourceStrategy = Strategies.ResourceStrategy.Default(context);
        }

        /// <summary>
        /// Creates a new instance of <see cref="BaseBuilder"/>
        /// </summary>
        /// <param name="name">Pulumi internal name of the component</param>
        /// <param name="context">Context generator this builder should use</param>
        /// <param name="cro"></param>
        public BaseBuilder(string name, ResourceContext context, CustomResourceOptions cro)
        {
            Name = name;
            Context = context;
            ResourceStrategy = Strategies.ResourceStrategy.Default(context);
            CustomResourceOptions = cro;
        }        
    }

    /// <summary>
    /// Stize infrastructure base builder
    /// </summary>
    public abstract class BaseBuilder<T> : BaseBuilder
    {
        /// <summary>
        /// Create a new instance of <see cref="BaseBuilder"/>
        /// </summary>
        /// <param name="name">Pulumi internal name of the component</param>
        public BaseBuilder(string name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="BaseBuilder"/>
        /// </summary>
        /// <param name="name">Pulumi internal name of the component</param>
        /// <param name="context">Context generator this builder should use</param>
        /// <returns></returns>
        public BaseBuilder(string name, ResourceContext context) : base(name, context)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="BaseBuilder"/>
        /// </summary>
        /// <param name="name">Pulumi internal name of the component</param>
        /// <param name="cro"></param>
        public BaseBuilder(string name, CustomResourceOptions cro) : base(name, cro)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="BaseBuilder"/>
        /// </summary>
        /// <param name="name">Pulumi internal name of the component</param>
        /// <param name="context">Context generator this builder should use</param>
        /// <param name="cro"></param>
        public BaseBuilder(string name, ResourceContext context, CustomResourceOptions cro) : base(name, context, cro)
        {
        }     

        /// <summary>
        /// Generates the Pulumi object using builder's <see cref="CustomResourceOptions"/>
        /// </summary>
        public virtual T Build()
        {
            return Build(CustomResourceOptions);
        }

        /// <summary>
        /// Generates the Pulumi object using the given <see cref="CustomResourceOptions"/>
        /// </summary>
        /// <param name="cro">Pulumi's <see cref="CustomResourceOptions"/></param>
        /// <returns></returns>
        public abstract T Build(CustomResourceOptions cro);
    }
}
