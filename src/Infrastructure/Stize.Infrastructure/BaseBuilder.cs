using System;
using Pulumi;
using Pulumi.Random;

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
        /// RandomId used to generate the name of this component
        /// </summary>
        /// <value></value>
        public RandomId RandomId {get; set;}

        /// <summary>
        /// Pulumi Custom Resource Options
        /// </summary>
        public CustomResourceOptions CustomResourceOptions { get; private set; } = new CustomResourceOptions();

        /// <summary>
        /// Creates a new instance of <see cref="BaseBuilder"/>
        /// </summary>
        /// <param name="name">Pulumi internal name of the component</param>
        public BaseBuilder(string name)
        {
            this.Name = name;
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
        /// <param name="cro"></param>
        public BaseBuilder(string name, CustomResourceOptions cro) : base(name, cro)
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
