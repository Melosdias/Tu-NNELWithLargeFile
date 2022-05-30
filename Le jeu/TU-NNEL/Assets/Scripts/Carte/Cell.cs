public class Cell
{
    public bool isRock;
    public bool isSpawn;
    public bool isRuin;
    

    public Cell(bool isRock, bool isSpawn, bool isRuin) 
    {
        this.isRock = isRock;
        this.isSpawn = isSpawn;
        this.isRuin = isRuin;
    }
}