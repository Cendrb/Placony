using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Util
{
    class ImmutableCollection<T> : IEnumerable<T>
    {
        private IEnumerable<T> innerCollection;

        public ImmutableCollection(IEnumerable<T> innerCollection)
        {
            this.innerCollection = innerCollection;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.innerCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
