
abstract class Actor
{
    private string name = null;
    public string Name => name;
    private int id = 0;
    public int Id => id;
    protected int party = 0;
    protected Inventory inventory = null;
}

class HumanPlayer : Actor
{

}

class AIPlayer : Actor
{

}