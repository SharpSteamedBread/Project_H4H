using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBeam : MonoBehaviour
{    
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float rayDistance = 100f;

    [Header("ºö ÀÌÆåÆ® À§Ä¡: Ground_Beam")]
    [SerializeField] private Transform objParent;
    [SerializeField] private Transform objBeamGround;
    [SerializeField] private float locationBeamGroundFlow = 1f;

    [Header("ºö ÀÌÆåÆ® À§Ä¡: round_Back")]
    [SerializeField] private Transform objBeamRound;
    [SerializeField] private float locationBeamRoundFlow = 0f;

    void FixedUpdate()
    {
        if(gameObject.CompareTag("Object_Beam_Left"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(1, 0, 0), Mathf.Infinity, groundLayer);

            Debug.DrawRay(transform.position, new Vector3(1 * hit.distance, 0, 0), Color.white);

            Debug.Log($"hit Point: {hit.point}, °Å¸®: {hit.distance}, hit Point X: {hit.point.x}, hit Point Y: {hit.point.y}");

            objBeamGround.position = new Vector2(hit.point.x + locationBeamGroundFlow, objBeamGround.position.y);
            objBeamRound.position = new Vector2(hit.point.x + locationBeamRoundFlow, objBeamGround.position.y);
        }

        else if(gameObject.CompareTag("Object_Beam_Right"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(-1, 0, 0), Mathf.Infinity, groundLayer);

            Debug.DrawRay(transform.position, new Vector3(-1 * hit.distance, 0, 0), Color.white);

            Debug.Log($"hit Point: {hit.point}, °Å¸®: {hit.distance}, hit Point X: {hit.point.x}, hit Point Y: {hit.point.y}");

            objBeamGround.position = new Vector2(hit.point.x + locationBeamGroundFlow, objBeamGround.position.y);
            objBeamRound.position = new Vector2(hit.point.x + locationBeamRoundFlow, objBeamGround.position.y);
        }

        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, Mathf.Infinity, groundLayer);

            Debug.DrawRay(transform.position, new Vector3(0, -1 * hit.distance, 0), Color.white);

            Debug.Log($"hit Point: {hit.point}, °Å¸®: {hit.distance}, hit Point X: {hit.point.x}, hit Point Y: {hit.point.y}");

            objBeamGround.position = new Vector2(objBeamGround.position.x, hit.point.y + (objParent.position.y - 11.26f) + locationBeamGroundFlow);
            objBeamRound.position = new Vector2(objBeamRound.position.x, hit.point.y + locationBeamRoundFlow);
        }
        
    }
}
