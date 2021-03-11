namespace interpx

module Interp2D =
    
    type Grid (sw:double, nw:double, ne:double, se:double, x1:double, x2:double, y1:double, y2:double) =
        member this.SW = sw
        member this.NW = nw
        member this.NE = ne
        member this.SE = se
        member this.X1 = x1
        member this.X2 = x2
        member this.Y1 = y1
        member this.Y2 = y2

    let Get (g:Grid, x:double, y:double) : double =
        let x2x1 = g.X2 - g.X1
        let y2y1 = g.Y2 - g.Y1
        let x2x = g.X2 - x
        let y2y = g.Y2 - y
        let yy1 = y - g.Y1
        let xx1 = x - g.X1
        1.0 / (x2x1 * y2y1) * (g.SW * x2x * y2y + g.SE * xx1 * y2y + g.NW * x2x * yy1 + g.NE * xx1 * yy1)
