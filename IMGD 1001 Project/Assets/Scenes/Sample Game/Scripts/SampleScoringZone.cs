using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SampleScoringZone : MonoBehaviour
{
    public EventTrigger.TriggerEvent sampleScoreTrigger;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SampleBall ball = collision.gameObject.GetComponent<SampleBall>();

        if (ball != null)
        {
            BaseEventData eventData = new BaseEventData(EventSystem.current);
            this.sampleScoreTrigger.Invoke(eventData);
        }
    }
}
