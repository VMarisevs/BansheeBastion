   private Vector2 getNextStep()
    {
        if (findPath((int)transform.position.x + 1, (int)transform.position.y ,(int)transform.position.x, (int)transform.position.y,0) )
            return new Vector2(1, 0);

        if (findPath((int)transform.position.x , (int)transform.position.y - 1,(int)transform.position.x, (int)transform.position.y,0) )
            return new Vector2(0, -1);
       
       if (findPath((int)transform.position.x, (int)transform.position.y + 1, (int)transform.position.x, (int)transform.position.y,0 ))
            return new Vector2(0, 1);
       //if (findPath((int)transform.position.x-1, (int)transform.position.y))
       //     return new Vector2(-1, 0);

        return Vector2.zero;
    
    }

    private bool findPath(int x, int y , int oldX, int oldY, int count)
    {

        count++;
        if(count > 50)
            return false;


        print("X: " + (x - oldX) + "  Y: " + (y - oldY));
        //print("x=" + x + " y=" + y);
        // 1 if (x,y outside maze) return false
        if (x < 0 || x >= MapManager.xCol || y < 0 || y >= MapManager.yRow)
            return false;

        // 2 if (x,y is goal) return true
        // close to goal!!!
        if  (atTheTarget(x,y))            
            return true;


 
        // 3 if (x,y not open) return false
      //  if (MapManager.pathArray[x, y] != '.')
          //  return false;

        // 4 mark x,y as part of solution path
     //   MapManager.pathArray[x, y] = '+';

        if (findPath(x + 1, y , x,y,count))
            return true;

        if (y - 1 != oldY && x != oldX &&  findPath(x, y - 1, x, y, count))
            return true;
       
        if ( y +1 != oldY && x != oldX && findPath(x, y + 1, x, y, count))
            return true;
       // if (findPath(x - 1, y ))
       //    return true;

        // unmark x,y as part of solution path
       // MapManager.pathArray[x, y] = 'x';

        return false;
    }