import React from 'react';
import * as Diff from 'diff';

interface DiffComponentProps {
    translatedText: string;
    correctedText: string;
}

const DiffComponent: React.FC<DiffComponentProps> = ({ translatedText, correctedText }) => {
    const diff = Diff.diffWords(translatedText, correctedText);

    return (
        <div>
            <p className="text-base">
                {diff.map((part, index) => {
                    const className = part.added
                        ? 'bg-green-200'
                        : part.removed
                            ? 'bg-red-200 line-through'
                            : '';
                    return (
                        <span key={index} className={className}>{part.value}</span>
                    );
                })}
            </p>
        </div>
    );
};

export default DiffComponent;