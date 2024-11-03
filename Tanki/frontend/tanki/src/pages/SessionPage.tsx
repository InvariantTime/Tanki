import { GameField } from "../components/GameField";
import { SessionEditor } from "../components/SessionEditor";
import { PlayerList } from "../components/PlayerList";
import { useEffect, useState } from "react";
import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { Player } from "../models/Player";
import { useParams } from "react-router-dom";

export const SessionPage = () => {

    const [players, setPlayers] = useState<Player[]>([]);
    const { sessionId } = useParams<{sessionId:string}>();

    const onPlayerChanged = (players: Player[]) =>
    {
        setPlayers(players.sort((a, b) => a.score > b.score ? -1 : 1));
    }

    const initConnection = async () => {

        const builder = new HubConnectionBuilder()
            .withUrl(`http://localhost:5074/ws/gameSession?sessionId=${sessionId}`)
            .configureLogging(LogLevel.Information);

        const connection = builder.build();
        connection.on("playersChanged", onPlayerChanged);

        await connection.start();
        await connection.invoke("join");
    }


    useEffect(() => {

        if (!sessionId)
            return;

        initConnection();
    }, [sessionId]);

    return (
        <div className="p-3 w-full">
            <div className="h-full flex justify-between
                 flex-wrap py-4 gap-2">

                <div className="bg-white rounded border
                    border-slate-300 shadow-xl w-[20%] max-h-[50%]">

                    <PlayerList players={players}/>
                </div>

                <div className="w-[50%]">
                    <GameField />
                </div>

                <div className="w-[28%]">
                    <SessionEditor />
                </div>
            </div>
        </div>
    );
}