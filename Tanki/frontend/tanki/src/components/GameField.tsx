import { Text } from "@chakra-ui/react"
import { useEffect, useRef, useState } from "react"
import { drawTank } from "../services/Drawers/TankDrawer";
import { Tank } from "../models/GameModels";
import { GameScene } from "../models/GameScene";
import { drawBullet } from "../services/Drawing";

interface Props {

}

export const GameField = () => {
    const ref = useRef<HTMLCanvasElement>(null);
    const [scene, setScene] = useState<GameScene>();

    var tank : Tank = {
        position: {x: 100, y: 100},
        rotation: 0,
        headRotation: 0,
        owner: "Vadim"
    };

    useEffect(() => {
        const render = () => {

            const canvas = ref.current;

            if (canvas === null)
                return;

            const context = canvas.getContext("2d");

            if (context === null)
                return;

            context.clearRect(0, 0, canvas.width, canvas.height);
            context.save();

            drawBullet(context, tank);

            requestAnimationFrame(render);
        }

        render();
    }, []);

    return (
        <div className="bg-white rounded border
            border-slate-300 shadow-xl h-full p-2">

            <Text as="b">Game</Text>

            <canvas ref={ref} width={800} height={600} />
        </div>
    )
}