using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int score = 0;

    public int GetScore() => score;

    public void AddScore(int amount)
    {
        score += amount;
        Mathf.Clamp(score, 0, int.MaxValue);
    }

    public void ResetScore() => score = 0;
}
