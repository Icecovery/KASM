using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using UnityEngine.UI;

namespace KASM
{
    public class ProcessorEditor : MonoBehaviour
    {
        public GameObject nodePanelTemplate;

        private List<GameObject> panelNodes;

        private void Start()
        {
            PopulateNodePanelContent();
        }

        private void PopulateNodePanelContent()
        {
            panelNodes = new List<GameObject>();

            nodePanelTemplate.SetActive(false);
            Type[] types = GetTypes(Assembly.GetExecutingAssembly()).ToArray();

            foreach (Type type in types)
            {
                GameObject newNode = Instantiate(nodePanelTemplate);
                newNode.transform.SetParent(nodePanelTemplate.transform.parent);
                newNode.transform.Find("NodeName").gameObject.GetComponent<Text>().text = type.Name;
                newNode.transform.Find("NodeIcon/IconText").gameObject.GetComponent<Text>().text = type.Name[0].ToString();
                newNode.name = type.Name.ToLower();
                newNode.SetActive(true);
                panelNodes.Add(newNode);
            }
        }

        public void NodePanelSearch(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                input = input.Trim().ToLower();
                panelNodes.ForEach(x => x.SetActive(x.name.Contains(input)));
            }
            else
            {
                panelNodes.ForEach(x => x.SetActive(true));
            }
        }

        private IEnumerable<Type> GetTypes(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.Namespace == "KASM")
                {
                    yield return type;
                }
            }
        }
    }
}
