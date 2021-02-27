using System;
using System.Collections;
using System.Collections.Generic;

namespace Antilatency.UnityPrototypingTools {

    public class FieldChangeMonitor<T> : IEnumerable {

        public IEnumerator GetEnumerator() {
            yield break;
        }

        public void Add<TField>(Func<T, TField> selector, Action<T> action) {
            _closures.Add(Closure(selector, action).GetEnumerator());
        }

        public void Process(T target) {
            _target = target;
            foreach (var closure in _closures)
                closure.MoveNext();
        }

        private IEnumerable Closure<TField>(Func<T, TField> selector, Action<T> action) {
            var previousValue = selector(_target);
            yield return null;

            while (true) {
                var target = _target;
                var currentValue = selector(target);
                if (!object.Equals(currentValue, previousValue)) {
                    action(target);
                    previousValue = currentValue;
                }

                yield return null;
            }
        }

        private T _target;
        private List<IEnumerator> _closures = new List<IEnumerator>();
    }
}
