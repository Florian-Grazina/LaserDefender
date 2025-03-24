using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int score;

    protected void Awake()
    {
        ResetScore();
    }

    public int GetScore() => score;

    public void AddScore(int amount)
    {
        score += amount;
        Mathf.Clamp(score, 0, int.MaxValue);
    }

    public void ResetScore() => score = 0;
}
