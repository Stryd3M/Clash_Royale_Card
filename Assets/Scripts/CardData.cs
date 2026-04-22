using UnityEngine;

public enum CardRarity { Common, Rare, Epic, Legendary, Champion }
public enum MovementSpeed { Slow, Medium, Fast, VeryFast }

[CreateAssetMenu(fileName = "NewCard", menuName = "Clash Royale/Card Data")]
public class CardData : ScriptableObject
{
    public string cardName;
    public Sprite cardIcon;
    public int elixirCost;
    public CardRarity rarity;
    public string type;
    public string targets;
    public string rangeType;
    public float hitSpeed;
    public MovementSpeed movementSpeed;
    public int releaseYear;
}