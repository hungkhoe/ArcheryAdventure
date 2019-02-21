using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour {
    private static Pooling instance;
    public static Pooling Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject ga = new GameObject("Pooling");
                instance = ga.AddComponent<Pooling>();
            }

            return instance;
        }
    }

    private Transform mytransform;
    public Transform myTransform { get { return mytransform; } }

    private void Awake()
    {
        mytransform = transform;
        mytransform.position = new Vector2(0, 15);
    }

    public GameObject SpawnObject(string name, string path = "") // Path ".../"
    {
        return SpawnObject(name, true, path);
    }

    public GameObject SpawnObject(string name, bool isEnable, string path = "")
    {
        Transform tf = myTransform.Find(name);
        if (!tf)
        {
            GameObject ga = new GameObject(name);
            tf = ga.transform;
            tf.SetParent(myTransform);
            tf.localPosition = Vector3.zero;
        }

        if (tf.childCount == 0)
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/" + path + name) as GameObject) as GameObject;

            go.name = name;
            go.SetActive(isEnable);
            return go;
        }

        Transform tfGo = tf.GetChild(0);
        tfGo.parent = null;
        tfGo.gameObject.SetActive(isEnable);
        return tfGo.gameObject;
    }

    public GameObject SpawnObject(string name, Vector3 position, bool isEnable = true, string path = "")
    {
        return SpawnObject(name, position, Quaternion.identity, isEnable, path);
    }

    public GameObject SpawnObject(string name, Vector3 position, Quaternion rotation, bool isEnable = true, string path = "")
    {
        Transform tf = myTransform.Find(name);
        if (!tf)
        {
            GameObject ga = new GameObject(name);
            tf = ga.transform;
            tf.SetParent(myTransform);
            tf.localPosition = Vector3.zero;
        }

        if (tf.childCount == 0)
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/" + path + name) as GameObject, position, rotation) as GameObject;
            go.name = name;
            go.transform.position = position;
            go.SetActive(isEnable);
            return go;
        }

        Transform tfGo = tf.GetChild(0);
        tfGo.parent = null;
        tfGo.position = position;
        tfGo.rotation = rotation;
        tfGo.gameObject.SetActive(isEnable);
        return tfGo.gameObject;
    }

    public void ReturnObject(string name, GameObject obj, float delayTime)
    {
        if (delayTime <= 0)
            ReturnImmediately(name, obj);
        else
            StartCoroutine(ReturningObject(name, obj, delayTime));
    }

    private void ReturnImmediately(string name, GameObject obj)
    {
        Transform tf = myTransform.Find(name);
        if (!tf)
        {
            GameObject ga = new GameObject(name);
            tf = ga.transform;
            tf.SetParent(myTransform);
            tf.localPosition = Vector3.zero;
        }

        Transform objTF = obj.transform;
        obj.SetActive(false);
        objTF.SetParent(tf);
        objTF.localPosition = Vector3.zero;
        obj.name = name;
    }

    private IEnumerator ReturningObject(string name, GameObject obj, float delayTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);
        ReturnImmediately(name, obj);
    }

    //==================================================================================
    public GameObject SpawnEnemy(string name, bool active = true)
    {
        return SpawnObject(name, active, "Enemies/");
    }
}
