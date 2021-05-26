using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KASM
{
    public class TestClass : MonoBehaviour
    {
        public Text text;

        void Start()
        {

        }

        void Update()
        {

        }

        public void OnPress()
        {
            text.text = Random.Range(0, 100).ToString();
        }
    }
}