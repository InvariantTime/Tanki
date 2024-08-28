import { GameField } from "../components/GameField";
import { SessionEditor } from "../components/SessionEditor";
import { PlayerList } from "../components/PlayerList";

export const SessionPage = () => {


    
    return (
        <div className="p-3 w-full">
            <div className="h-full flex justify-between
                 flex-wrap py-4 gap-2">
                
                <div className="bg-white rounded border
                    border-slate-300 shadow-xl w-[20%] max-h-[50%]">
                    
                    <PlayerList/>
                </div>

                <div className="w-[50%]">
                    <GameField/>
                </div>

                <div className="w-[28%]">
                    <SessionEditor/>
                </div>
            </div>
        </div>
    );
}