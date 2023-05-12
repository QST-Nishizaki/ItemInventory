namespace ItemInventory
{
    public record Employee
    (
        int[] fav,
        int id, 
        string name, 
        int status, 
        int honseki, 
        int location, 
        string comment
        //int roulette,   
        //int checkin     //チェックインしたかどうか真偽値で判断
    );
    public record EmployeeNew
    (
        int[] fav,
        int id, 
        string name, 
        int status, 
        int honseki, 
        int location, 
        string comment
        //int roulette,   
        //int checkin
    );
}
