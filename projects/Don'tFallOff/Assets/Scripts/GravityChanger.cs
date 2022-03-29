using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityChan
{
    public class GravityChanger : MonoBehaviour
    {
        private GameObject player;
        private float jumpingPower;
        private float speedingPower;
        public float minSpeedForceModifier;
        public float maxSpeedForceModifier;

        public float minJumpForceModifier;
        public float maxJumpForceModifier;

        void Start()
        {
            player = GameObject.Find("unitychan");
            jumpingPower = player.GetComponent<UnityChanControlScriptWithRgidBody>().jumpPower;
            speedingPower = player.GetComponent<UnityChanControlScriptWithRgidBody>().forwardSpeed;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(ForceModifier());
            }
        }

        IEnumerator ForceModifier()
        {
            float calculatedJumpModifier = Random.Range(minJumpForceModifier, maxJumpForceModifier);
            float calculatedSpeedModifier = Random.Range(minSpeedForceModifier, maxSpeedForceModifier);
            player.GetComponent<UnityChanControlScriptWithRgidBody>().jumpPower = jumpingPower * calculatedJumpModifier;
            player.GetComponent<UnityChanControlScriptWithRgidBody>().forwardSpeed = speedingPower * calculatedSpeedModifier;
            yield return new WaitForSeconds(5);
            player.GetComponent<UnityChanControlScriptWithRgidBody>().jumpPower = jumpingPower;
            player.GetComponent<UnityChanControlScriptWithRgidBody>().forwardSpeed = speedingPower;
        }
    }
}
