using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [ExecuteAlways]
    public class BlurAnimationController : MonoBehaviour
    {

        [Range(0, 5f)]
        public float blurValue;
        
        //TODO implement coroutine
        void Update()=>
            GetComponent<Image>().material.SetFloat("_Size", blurValue);
        
    }
}
